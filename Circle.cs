using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace AccelCourse
{
    class Circle : Collider
    {
        private float m_radius;
        public float radius
        {
            get { return m_radius; }
        }

        public Circle(Vector2 _center, float _radius)
            : base(_center)
        {
            m_colliderType = ColliderType.Circle;
            m_radius = _radius;
        }


        public IntersectData IntersectsPoint(Point _other)
        {
            IntersectData IntersectData = new IntersectData();
            IntersectData.collider = _other;

            float distance = Vector2.Distance(_other.center, center);

            if (distance > radius)
            {
                IntersectData.collision = false;
                return IntersectData;
            }

            Vector2 direction = _other.center - center;
            direction.Normalize();

            IntersectData.Point = direction * m_radius;
            IntersectData.Normal = direction;

            return IntersectData;
        }

        public IntersectData IntersectsAABB(AABB _other)
        {
            IntersectData IntersectData = new IntersectData();
            IntersectData.collider = _other;

            return IntersectData;
        }
    }
}
