
using Injectify.Package.Container;
using Injectify.Package.Enums;
using System.ComponentModel;
using Container = Injectify.Package.Container.Container;
using Injectify.Package.Enums;
using System.Xml.Linq;

namespace TestProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Basic Registration and Resolution
            //var conatiner = new Container();
            //conatiner.Register<IService, Service>();
            //var serviceObj = conatiner.Resolve<IService>();
            //serviceObj.DoSomething();
            #endregion

            // Lifetime Management 
            var conatiner = new Container();

            Singleton
            conatiner.Register<IService, Service>(LifeTime.Singleton);
            var service1 = conatiner.Resolve<IService>();
            var service2 = conatiner.Resolve<IService>();
            if true that mean ref in the same location that mean only object create
            Console.WriteLine(object.ReferenceEquals(service1, service2));

            Transient
            conatiner.Register<IService, Service>(LifeTime.Transient);
            var service1 = conatiner.Resolve<IService>();
            var service2 = conatiner.Resolve(typeof(IService));
            //  if false that mean ref are created each time requested !
            Console.WriteLine(object.ReferenceEquals(service1, service2));


            // Scoped 
            conatiner.Register<IService, Service>(LifeTime.Scoped);
            IService service1;
            using ( var scope1 =  conatiner.CreateScope() )
            {
                  service1 =  scope1.Resolve<IService>();
                var service2 = scope1.Resolve<IService>();

                //  should be true beacuse it deals as a singleton in the same scope 

                Console.WriteLine(object.ReferenceEquals(service1, service2));// Outputs: True
            }

            using (var scope2 = conatiner.CreateScope())
            {
                var service3 = scope2.Resolve<IService>();
                //  should be False beacuse it deals as a Transient in the defferent scope 
                Console.WriteLine(object.ReferenceEquals(service1, service3)); // Outputs: False
            }












            Console.ReadKey();
        }
    }

    public interface IService
    {
        void DoSomething();
    }

    public class Service : IService
    {
        public void DoSomething()
            => Console.WriteLine("Do Something");
    }


}
