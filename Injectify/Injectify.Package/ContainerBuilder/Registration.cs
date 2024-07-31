using Injectify.Package.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Injectify.Package.ContainerBuilder
{
    public  class Registration
    {
        public Type ImplementationType { get; private set; }

        public LifeTime LifeTime { get; private set; }

        public object Instance { get; set; } 
        public Registration(Type implementationType, LifeTime lifeTime)
        {
            ImplementationType = implementationType;
            LifeTime = lifeTime;
        }

        public object GetInstance(Func<Type, object > factory )
        {
            switch (LifeTime)
            {
                case LifeTime.Singleton:
                    if (Instance == null)
                    {
                        Instance = factory(ImplementationType);
                    }
                    return Instance;
                case LifeTime.Transient:
                    return factory(ImplementationType);
                default:
                    throw new NotImplementedException("Scoped lifetime not yet implemented");
            }


        }





    }
}
