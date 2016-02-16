using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace AccelCourse
{
    enum Physics_MassType
    {
        Normal,
        Infitive,
        Zero,
        DontInteract
    }

    class PhysicsObject : GameObjectComponent
    {
        private Vector2 m_acceleration;
        public Vector2 a
        {
            get { return m_acceleration; }
            set { m_acceleration = value; }
        }

        private Vector2 m_velcoity;
        public Vector2 v
        {
            get { return m_velcoity; }
            set { m_velcoity = value; }
        }

        private float m_mass;
        public float m
        {
            get { return m_mass; }
            set { m_mass = value; }
        }

        private Physics_MassType m_massType;
        public Physics_MassType massType
        {
            get { return m_massType; }
            set { m_massType = value; }
        }

        private float m_angularAcceleration;
        public float alpha
        {
            get { return m_angularAcceleration; }
            set { m_angularAcceleration = value; }
        }

        private float m_angularVelocity;
        public float omega
        {
            get { return m_angularVelocity; }
            set { m_angularVelocity = value; }
        }

        private float m_inertia;
        public float I
        {
            get { return m_inertia; }
            set { m_inertia = value; }
        }

        private float m_friction;
        public float myu
        {
            get { return m_friction; }
            set { m_friction = value; }
        }

        private float m_bouncyness;
        public float bouncyness
        {
            get { return m_bouncyness; }
            set { m_bouncyness = value; }
        }

        private List<Vector2> m_forces;

        private List<PhysicsObjectComponent> m_physicsObjectComponents;
        private PhysicCore m_physicsCore;

        public PhysicsObject(PhysicCore _physicsCore)
        {
            m_physicsCore = _physicsCore;
            m_physicsCore.AddPhysicsObject(this);
            m_physicsObjectComponents = new List<PhysicsObjectComponent>();
            m_forces = new List<Vector2>();
        }

        public void AddForce(Vector2 _force)
        {
            m_forces.Add(_force);
        }

        public void Edpoch(float _deltaTime)
        {
            if (m_massType != Physics_MassType.Infitive)
            {
                a = Vector2.Zero;
                a += m_physicsCore.gravitation;
                foreach (Vector2 force in m_forces)
                    a += force / m;


                v += a * _deltaTime;
                gameObject.transform.position += v * _deltaTime;

                omega += alpha * _deltaTime;
                gameObject.transform.rotation += omega * _deltaTime;
                m_forces.Clear();

            }
            foreach (PhysicsObjectComponent pc in m_physicsObjectComponents)
                pc.Epoch(_deltaTime);
        }

        public void AddPhysicsComponent(PhysicsObjectComponent _pc)
        {
            if (!m_physicsObjectComponents.Contains(_pc))
            {
                m_physicsObjectComponents.Add(_pc);
                _pc.physicsObject = this;
            }
        }

        public T GetPhysicsComponent<T>() where T : PhysicsObjectComponent
        {
            foreach (PhysicsObjectComponent pc in m_physicsObjectComponents)
            {
                Type pcType = pc.GetType();
                if (pc.derivedTypes.Contains(typeof(T)))
                    return (T)pc;
            }
            return null;
        }

        private void Collision(object[] _IntersectData)
        {
            Collision((IntersectData)_IntersectData[0]);
        }

        protected virtual void Collision(IntersectData _IntersectData)
        {

        }
    }
}
