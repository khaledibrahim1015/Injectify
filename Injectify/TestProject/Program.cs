
using Injectify.Package.Container;

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
