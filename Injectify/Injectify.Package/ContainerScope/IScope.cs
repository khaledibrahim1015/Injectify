using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Injectify.Package.ContainerScope
{
    public interface IScope : IDisposable
    {
        object Resolve(Type serviceType);
        T Resolve<T>();

    }
}
