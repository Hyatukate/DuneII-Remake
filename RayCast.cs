using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace AccelCourse
{
    class RayCast : Collider
    {
        private Vector2 m_direction;
        public Vector2 direction
        {
            get { return m_direction; }
        }

        private float m_distance;
        public float distance
        {
            get { return m_distance; }
        }

        public RayCast(Vector2 _center, Vector2 _direction, float _distance)
            : base(_center)
        {
            m_colliderType = ColliderType.RayCast;
            m_direction = _direction;
            m_distance = _distance;
        }
    }
}
