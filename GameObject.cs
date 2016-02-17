using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AccelCourse
{
    class GameObject
    {
        private Transform m_transform;
        public Transform transform
        {
            get { return m_transform; }
        }

        private List<GameObject> m_childs;
        public List<GameObject> childs
        {
            get { return m_childs; }
        }

        private List<GameObjectComponent> m_components;
        public List<GameObjectComponent> components
        {
            get { return m_components; }
        }

        private bool m_active;
        public bool active
        {
            get { return m_active; }
            set { m_active = value; }
        }
        
        public GameObject()
        {
            m_childs = new List<GameObject>();
            m_components = new List<GameObjectComponent>();

            m_transform = (Transform)AddComponent(new Transform());
            m_transform.gameObject = this;

            m_active = true;
        }

        public void Initialize()
        {
            foreach (GameObject go in m_childs)
                go.Initialize();
            //foreach (GameObjectComponent goc in m_components)
               // goc.Initialize();

            if (m_transform.parent != null)
                m_transform.parent.gameObject.AddChildren(this);
        }

        public void Update(GameTime _gameTime)
        {
            if (m_active)
            {
                foreach (GameObject go in m_childs)
                    go.Update(_gameTime);
                foreach (GameObjectComponent goc in m_components)
                    goc.Update(_gameTime);
            }
        }

        public void Draw(RenderingCore _renderingCore)
        {
            if (m_active)
            {
                foreach (GameObject go in m_childs)
                    go.Draw(_renderingCore);
                foreach (GameObjectComponent goc in m_components)
                    goc.Draw(_renderingCore);
            }
        }

        public GameObject AddChildren(GameObject _child)
        {
            if (!m_childs.Contains(_child))
            {
                m_childs.Add(_child);
            }
            return this;
        }

        public bool RemoveChildren(GameObject _child)
        {
            if (m_childs.Contains(_child))
            {
                m_childs.Add(_child);
                return true;
            }
            return false;
        }

        public GameObject AddComponents(GameObjectComponent _goc)
        {
            m_components.Add(_goc);
            _goc.gameObject = this;
            _goc.Initialize();
            return this;
        }

        public GameObjectComponent AddComponent(GameObjectComponent _goc)
        {
            m_components.Add(_goc);
            _goc.gameObject = this;
            _goc.Initialize();
            return _goc;
        }

        public T GetComponent<T>() where T:GameObjectComponent
        {
            foreach(GameObjectComponent goc in m_components)
                if(goc.GetType().Equals(typeof(T)))
                    return (T)goc;
            return null;
        }

        public void InvokeMethod(string _methodname)
        {
            InvokeMethod(_methodname, null);
        }

        public void InvokeMethod(string _methodname, object[] _parameters)
        {
            this.GetType().GetMethod(_methodname).Invoke(this, _parameters);
        }

        public void Collision(IntersectData _IntersectData)
        {

        }
    }
}
