using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AccelCourse
{
    class GameObjectComponent
    {
        private GameObject m_gameObject;
        public GameObject gameObject
        {
            get { return m_gameObject; }
            set { m_gameObject = value; }
        }

        public GameObjectComponent()
        {
        }

        public virtual void Initialize()
        {

        }

        public virtual void Update(GameTime _gameTime)
        {

        }

        public virtual void Draw(RenderingCore _renderingCore)
        {
        }

        public virtual void InvokeMethod(string _methodname)
        {
            InvokeMethod(_methodname, null);
        }

        public virtual void InvokeMethod(string _methodname, object[] _parameters)
        {
            this.GetType().GetMethod(_methodname).Invoke(this, _parameters);
        }
    }
}
