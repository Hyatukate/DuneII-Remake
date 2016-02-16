using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AccelCourse
{
    class PhysicsDebug : GameObjectComponent
    {
        public override void Draw(RenderingCore _renderingCore)
        {
            SpriteBatch batch = _renderingCore.spriteBatch;

            //Is there a PhysicsObject?
            PhysicsObject po = gameObject.GetComponent<PhysicsObject>();

            if (po == null)
                return;

            //Is there a collider?
            Collider c = po.GetPhysicsComponent<Collider>();

            if (c == null)
            {
                //Draw a point?
            }
            else
            {

            }
        }
    }
}
