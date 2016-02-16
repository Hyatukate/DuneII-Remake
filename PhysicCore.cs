using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace AccelCourse
{
    class PhysicCore
    {
        private Vector2 m_gravitation;
        public Vector2 gravitation
        {
            get { return m_gravitation; }
            set { m_gravitation = value; }
        }

        private List<PhysicsObject> m_physicsObjects;

        public PhysicCore(Vector2 _gravitation)
        {
            m_gravitation = _gravitation;
            m_physicsObjects = new List<PhysicsObject>();
        }

        public void Epoch(float _deltaTime)
        {
            foreach (PhysicsObject po in m_physicsObjects)
                po.Edpoch(_deltaTime);
        }

        public void ResolvePhysics()
        {
            List<PhysicsObject> copyList = new List<PhysicsObject>(m_physicsObjects);

            foreach (PhysicsObject that in m_physicsObjects)
            {
                copyList.Remove(that);
                foreach (PhysicsObject other in copyList)
                {
                    Collider thatc = that.GetPhysicsComponent<Collider>();
                    Collider otherc = other.GetPhysicsComponent<Collider>();
                    if (thatc == null || otherc == null)
                        break;

                    IntersectData IntersectData = new IntersectData();
                    if (thatc.colliderType == ColliderType.Point && otherc.colliderType == ColliderType.Point)
                    {
                        IntersectData = ((Point)thatc).IntersectsPoint((Point)otherc);
                        if (IntersectData.collision)
                            CalculateResolution(that, other, IntersectData);
                    }
                    else if (thatc.colliderType == ColliderType.AABB && otherc.colliderType == ColliderType.Point)
                    {
                        IntersectData = ((AABB)thatc).IntersectsPoint((Point)otherc);
                        if (IntersectData.collision)
                            CalculateResolution(that, other, IntersectData);
                    }
                    else if (thatc.colliderType == ColliderType.AABB && otherc.colliderType == ColliderType.AABB)
                    {
                        IntersectData = ((AABB)thatc).IntersectsAABB((AABB)otherc);
                        if (IntersectData.collision)
                            CalculateResolution(that, other, IntersectData);
                    }
                    /*
                    else if (thatc.colliderType == ColliderType.AABB && otherc.colliderType == ColliderType.Circle)
                    {
                        IntersectData = ((AABB)thatc).IntersectsCircle((Circle)otherc);
                        if (IntersectData.collision)
                            CalculateResolution(that, other, IntersectData);
                    }*/
                    else if (thatc.colliderType == ColliderType.Circle && otherc.colliderType == ColliderType.Point)
                    {
                        IntersectData = ((Circle)thatc).IntersectsPoint((Point)otherc);
                        if (IntersectData.collision)
                            CalculateResolution(that, other, IntersectData);
                    }
/*
                    else if (thatc.colliderType == ColliderType.Circle && otherc.colliderType == ColliderType.Circle)
                    {
                        IntersectData = ((Circle)thatc).IntersectsCircle((Circle)otherc);
                        if (IntersectData.collision)
                            CalculateResolution(that, other, IntersectData);
                    }
                    */
                    if (IntersectData.collision)
                    {
                        object[] para = new object[1];
                        para[0] = IntersectData;
                        thatc.physicsObject.gameObject.InvokeMethod("Collision", para);
                        IntersectData.collider = thatc;
                        otherc.physicsObject.gameObject.InvokeMethod("Collision", para);
                    }
                }
            }
            
        }

        private void CalculateResolution(PhysicsObject that, PhysicsObject other, IntersectData _IntersectData)
        {
            if (that.massType == Physics_MassType.DontInteract || other.massType == Physics_MassType.DontInteract)
                return;
            
            //We want that 'that' is always the one moving towards 'other'

            if (that.v.LengthSquared() < other.v.LengthSquared())
            {
                PhysicsObject tmp = that;
                that = other;
                other = tmp;
            }

            Vector2 thatov = that.v;
            Vector2 otherov = that.v;
            float k = (that.bouncyness + other.bouncyness) / 2f;

            if (Vector2.Dot(that.v, _IntersectData.Normal) != 0f)
            {
                if (that.massType == Physics_MassType.Zero || other.massType == Physics_MassType.Infitive)
                {
                    that.v = (that.v - new Vector2(that.v.X * Math.Abs(_IntersectData.Normal.X), that.v.Y * Math.Abs(_IntersectData.Normal.Y)));
                    //Vector2 friction = Vector2.Normalize((float)Math.Sin(that.v.X) * new Vector2(_IntersectData.Normal.Y , _IntersectData.Normal.X));
                    //that.AddForce(friction);
                    //Console.WriteLine(friction.ToString());
                }
                else if (that.massType == Physics_MassType.Infitive || other.massType == Physics_MassType.Zero)
                {
                    other.v = -other.v * k;
                }
                else
                {
                    that.v = (that.m * that.v + other.m * other.v - other.m * (that.v - other.v) * k) / (that.m + other.m);
                    other.v = (other.m * other.v + that.m * thatov - that.m * (other.v - thatov) * k) / (that.m + other.m);
                }
            }

            that.gameObject.transform.position -= _IntersectData.Delta;
            /*
            if (that.massType == other.massType)
            {

                

                that.v = other.v * that.bouncyness + thatov * (1 - that.bouncyness);

                other.v = that.v * other.bouncyness + otherov * (1 - other.bouncyness);

                that.gameObject.transform.position += _IntersectData.Delta * _IntersectData.Normal;
                other.gameObject.transform.position -= _IntersectData.Delta * _IntersectData.Normal;

            }else if (that.massType == Physics_MassType.Zero || other.massType == Physics_MassType.Infitive)
            {
                that.v = (-that.v + (other.v - that.v)) * that.bouncyness;

                that.gameObject.transform.position -= _IntersectData.Delta * _IntersectData.Normal;
            }
            else if (other.massType == Physics_MassType.Zero || that.massType == Physics_MassType.Infitive)
            {
                other.v = (-other.v + (that.v - other.v)) * that.bouncyness;

                other.gameObject.transform.position -= _IntersectData.Delta * _IntersectData.Normal;
            }else
            {
            }*/
        }

        public void AddPhysicsObject(PhysicsObject _po)
        {
            if (!m_physicsObjects.Contains(_po))
                m_physicsObjects.Add(_po);
        }
    }
}
