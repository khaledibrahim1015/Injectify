﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Injectify.Package.Container
{
    public  interface IContainer
    {
        //Generic 

        /// <summary>
        /// To Register Services and its implementation 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        void Register<TInterface, TImplementation>() where TImplementation : TInterface;
        /// <summary>
        /// to Get object of its impl type 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>();

        // static 
        void  Register(Type serviceType , Type implementationType);

        object Resolve(Type serviceType);


    }
}