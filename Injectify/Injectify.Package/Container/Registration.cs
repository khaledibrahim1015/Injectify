using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Injectify.Package.Container
{
    public  class Registration
    {
        public Type ImplementationType { get; private set; }
        public Registration(Type implementationType)
        {
            ImplementationType = implementationType;
        }

    }
}
