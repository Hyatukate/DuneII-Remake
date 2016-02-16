using Microsoft.Xna.Framework;

namespace AccelCourse
{
    class Point : Collider
    {
        public Point(Vector2 m_center) : base(m_center) { m_colliderType = ColliderType.Point; }

        public IntersectData IntersectsPoint(Point _other)
        {
            IntersectData IntersectData = new IntersectData();
            IntersectData.collider = _other;

            if (center != _other.center)
            {
                IntersectData.collision = false;
                return IntersectData;
            }

            IntersectData.Point = center;
            IntersectData.Delta = Vector2.Zero;
            IntersectData.Normal = -physicsObject.v;
            IntersectData.Normal.Normalize();

            IntersectData.collision = true;
            return IntersectData;
        }
    }
}
