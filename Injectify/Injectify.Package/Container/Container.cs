using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Injectify.Package.Container
{
    public class Container : IContainer
    {
        private readonly ConcurrentDictionary<Type, Registration> _registrations = new ConcurrentDictionary<Type, Registration>();


        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            Register(typeof(TInterface), typeof(TImplementation));
        }

        public void Register(Type serviceType, Type implementationType)
        {
            if(!_registrations.TryAdd(serviceType , new Registration(implementationType)))
            {
                throw new InvalidOperationException($"Type {serviceType} already registered !");
            }
        }

        public T Resolve<T>()
           =>  (T)Resolve(typeof(T));


        public object Resolve(Type serviceType)
        {
            if(!_registrations.TryGetValue(serviceType , out var registration))
                throw new InvalidOperationException($"No Registeration Found for {serviceType}");

            //  here will take Registrartion and create New Instance 
            return CreateInstance(registration.ImplementationType);

        }

        private object CreateInstance(Type implementationType)
        {
            // Create Instance =>  Calling Invoke on a ConstructorInfo instance. || Using Activator.CreateInstance().
            // basic constructor injection

            //  Constructor selection prefers the constructor with the most parameters, which is a common DI container behavior.
            ConstructorInfo constructor = implementationType.GetConstructors()
                                        .OrderByDescending(ctorInfo => ctorInfo.GetParameters().Length).First();

            var parameter = constructor.GetParameters().Select(paramInfo => Resolve(paramInfo.ParameterType)).ToArray();
           return constructor.Invoke(parameter);

        }


    }
}
