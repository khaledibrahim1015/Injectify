using Injectify.Package.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Injectify.Package.ContainerBuilder
{
    public  interface IContainer
    {
        //Generic 

        /// <summary>
        /// To Register Services and its implementation 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        void Register<TInterface, TImplementation>(LifeTime lifeTime) where TImplementation : TInterface;
        /// <summary>
        /// to Get object of its impl type 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>();

        // static 
        void  Register(Type serviceType , Type implementationType, LifeTime lifeTime);

        object Resolve(Type serviceType);


    }
}
