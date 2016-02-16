using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace AccelCourse
{
    enum ColliderType
    {
        Point,
        AABB,
        Circle,
        RayCast,
    }

    struct IntersectData
    {
        public bool collision;
        public Collider collider;
        public Vector2 Point;
        public Vector2 Normal;
        public Vector2 Delta;
    }

    class Collider : PhysicsObjectComponent
    {
        private Vector2 m_center;
        public Vector2 center
        {
            get { return m_center; }
            set { m_center = value; }
        }

        protected ColliderType m_colliderType;
        public ColliderType colliderType
        {
            get { return m_colliderType; }
        }

        private int m_layer; //layer 0 collids with everything -1 with nothing
        public int layer
        {
            get { return m_layer; }
            set { m_layer = value; }
        }

        public Collider(Vector2 _center, int _layer = 0)
        {
            m_center = center;
            m_layer = _layer;
        }

        public override void Epoch(float _deltaTime)
        {
            m_center = physicsObject.gameObject.transform.position;
        }
    }
}
