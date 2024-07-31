using Injectify.Package;
using Injectify.Package.ContainerBuilder;

namespace TestProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Basic Registration and Resolution
            var conatiner = new Container();
            conatiner.Register<IService, Service>();
            var serviceObj = conatiner.Resolve<IService>();
            serviceObj.DoSomething();
            #endregion

            #region  Lifetime Management 
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
            using (var scope1 = conatiner.CreateScope())
            {
                service1 = scope1.Resolve<IService>();
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
            #endregion
            #region Property Injection 



            var conatiner = new Container();
            conatiner.Register<IService, Service>();
            conatiner.Register<Client, Client>();
            var client = conatiner.Resolve<Client>();
            Console.WriteLine(client.Service != null);




            #endregion






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


    // Test Property Injection 
    public class Client
    {
        [Inject]
        public IService Service { get; set; }




    }

}
