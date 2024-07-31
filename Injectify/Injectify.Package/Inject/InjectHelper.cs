using Injectify.Package.ContainerBuilder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Injectify.Package.Inject
{
    public static class InjectHelper
    {

        public static void InjectProperties( Container container,  object instance  )
        {
            var type = instance.GetType();
            IEnumerable<PropertyInfo> properties = type.GetProperties()
                                                         .Where(propInfo => propInfo.CanWrite
                                                             && propInfo.GetCustomAttribute<InjectAttribute>() != null);

            properties.ToList().ForEach(propInfo =>
            {
                var value = container.Resolve(propInfo.PropertyType);

                propInfo.SetValue(instance,value);
            });
        }

        public static void InjectMethods(Container container, object instance)
        {
            var type = instance.GetType();
            IEnumerable<MethodInfo> methods = type.GetMethods()
                                                    .Where(methodInfo => methodInfo.GetCustomAttribute<InjectAttribute>() != null);

            methods.ToList().ForEach(methodInfo =>
            {
                var parameters = methodInfo.GetParameters()
                                                .Select(paramInfo => container.Resolve(paramInfo.ParameterType)).ToArray();
                methodInfo.Invoke(instance, parameters);
            });

        }




    }
}
