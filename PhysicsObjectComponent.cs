using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccelCourse
{
    class PhysicsObjectComponent
    {
        private PhysicsObject m_physicsObject;
        public PhysicsObject physicsObject
        {
            get { return m_physicsObject; }
            set { m_physicsObject = value; }
        }

        private Type[] m_derivedTypes;
        public Type[] derivedTypes
        {
            get { return m_derivedTypes; }
        }

        public PhysicsObjectComponent()
        {
            m_derivedTypes = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                              from assemblyType in domainAssembly.GetTypes()
                              where typeof(PhysicsObjectComponent).IsAssignableFrom(assemblyType)
                              select assemblyType).ToArray();
        }

        public virtual void Epoch(float _deltaTime)
        {

        }
    }
}
