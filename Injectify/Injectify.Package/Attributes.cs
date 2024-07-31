using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Injectify.Package
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method , AllowMultiple =false)]
    public class InjectAttribute : Attribute { }


}
