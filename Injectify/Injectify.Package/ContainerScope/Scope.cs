using Injectify.Package.Container;
using Injectify.Package.Enums;
using System.Collections.Concurrent;
namespace Injectify.Package.ContainerScope
{
    public class Scope : IScope
    {
        private readonly ConcurrentDictionary<Type , object > _scopedInstances = new ConcurrentDictionary<Type , object >();    

        private readonly Injectify.Package.Container.Container _container;
        public Scope(Injectify.Package.Container.Container container)
        {
            _container = container;
        }


        public T Resolve <T> ()
            => (T) Resolve(typeof(T));


        public object Resolve(Type serviceType)
        {
            if(_container._registrations.TryGetValue(serviceType, out var registration))
            {
                if (registration.LifeTime == LifeTime.Scoped)
                    return _scopedInstances.GetOrAdd(serviceType, _ => _container.CreateInstance(registration.ImplementationType));
            }
            return registration.GetInstance(_container.CreateInstance);

        }



        public void Dispose()
        {
            foreach (var instance in _scopedInstances.Values)
            {
                (instance as IDisposable)?.Dispose();
            }
            _scopedInstances.Clear();
        }


    }
}
