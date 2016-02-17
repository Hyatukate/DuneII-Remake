using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace AccelCourse
{
    class Scene
    {
        public Scene()
        {
        }

        public virtual void LoadConent(ContentManager _content) { }
        public virtual void Initialize(out GameObject _root) { _root = new GameObject(); }
    }
}
