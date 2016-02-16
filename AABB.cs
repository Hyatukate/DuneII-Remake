using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace AccelCourse
{
    class AABB : Collider
    {
        private Vector2 m_dimensions;
        public Vector2 dimensions
        {
            get { return m_dimensions; }
            set { m_dimensions = value; }
        }

        public AABB(Vector2 _center, Vector2 _dim) : base(_center)
        {
            m_colliderType = ColliderType.AABB;
            m_dimensions = _dim;
        }

        public IntersectData IntersectsPoint(Point _other)
        {
            IntersectData IntersectData = new IntersectData();
            IntersectData.collider = _other;

            float deltaX = _other.center.X - center.X;
            float pointX = dimensions.X / 2 - Math.Abs(deltaX);

            if (pointX <= 0)
            {
                IntersectData.collision = false;
                return IntersectData;
            }

            float deltaY = _other.center.Y - center.Y;
            float pointY = dimensions.Y / 2 - Math.Abs(deltaY);

            if (pointY <= 0)
            {
                IntersectData.collision = false;
                return IntersectData;
            }

            if (pointX < pointY)
            {
                float sx = Math.Sign(deltaX);
                IntersectData.Delta = new Vector2(pointX * sx, 0f);
                IntersectData.Normal = new Vector2(sx, 0);
                IntersectData.Point = new Vector2(center.X + ((dimensions.X / 2) * sx), _other.center.Y);
            }
            else
            {
                float sy = Math.Sign(deltaY);
                IntersectData.Delta = new Vector2(0f, pointY * sy);
                IntersectData.Normal = new Vector2(0f, sy);
                IntersectData.Point = new Vector2(_other.center.X, center.Y + ((dimensions.X / 2) * sy));
            }

            IntersectData.collision = true;
            return IntersectData;
        }

        public IntersectData IntersectsAABB(AABB _other)
        {
            IntersectData IntersectData = new IntersectData();
            IntersectData.collider = _other;

            float deltaX = _other.center.X - center.X;
            float pointX = (_other.dimensions.X / 2 + dimensions.X / 2) - Math.Abs(deltaX);

            if (pointX <= 0)
            {
                IntersectData.collision = false;
                return IntersectData;
            }

            float deltaY = _other.center.Y - center.Y;
            float pointY = (_other.dimensions.Y / 2 + dimensions.Y / 2) - Math.Abs(deltaY);
            
            if (pointY <= 0)
            {
                IntersectData.collision = false;
                return IntersectData;
            }

            if (pointX < pointY)
            {
                float sx = Math.Sign(deltaX);
                IntersectData.Delta = new Vector2(pointX * sx, 0f);
                IntersectData.Normal = new Vector2(sx, 0);
                IntersectData.Point = new Vector2(center.X + ((dimensions.X / 2) * sx), _other.center.Y);
            }
            else
            {
                float sy = Math.Sign(deltaY);
                IntersectData.Delta = new Vector2(0f, pointY * sy);
                IntersectData.Normal = new Vector2(0f, sy);
                IntersectData.Point = new Vector2(_other.center.X, center.Y + ((dimensions.X / 2) * sy));
            }

            IntersectData.collision = true;
            return IntersectData;
        }

        public IntersectData IntersectsRayCast(RayCast _other)
        {
            IntersectData IntersectData = new IntersectData();
            IntersectData.collider = _other;

            float scaleX = 1.0f / (_other.direction.X * _other.distance);
            float scaleY = 1.0f / (_other.direction.Y * _other.distance);

            float signX = Math.Sign(scaleX);
            float signY = Math.Sign(scaleY);

            float nearTimeX = (center.X - signX * (dimensions.X / 2) - _other.center.X) * scaleX;
            float nearTimeY = (center.Y - signY * (dimensions.Y / 2) - _other.center.Y) * scaleY;

            float farTimeX = (center.X + signX * (dimensions.X / 2) - _other.center.X) * scaleX;
            float farTimeY = (center.Y + signY * (dimensions.Y / 2) - _other.center.Y) * scaleY;

            if (nearTimeX > farTimeY || nearTimeY > farTimeY)
            {
                IntersectData.collision = false;
                return IntersectData;
            }

            float nearTime = nearTimeX > nearTimeY ? nearTimeX : nearTimeY;
            float farTime = farTimeX > farTimeY ? farTimeX : farTimeY;

            float time = nearTime < 0 ? 0 : nearTime > 1 ? 1 : nearTime;

            if (nearTime >= 1 | farTime <= 0)
            {
                IntersectData.collision = false;
                return IntersectData;
            }

            if (nearTimeX > nearTimeY)
                IntersectData.Normal = new Vector2(-signX, 0);
            else
                IntersectData.Normal = new Vector2(0, -signY);
            IntersectData.Delta = new Vector2(time * (_other.direction.X * _other.distance), time * (_other.direction.Y * _other.distance));

            IntersectData.collision = true;
            return IntersectData;
        }
    }
}
