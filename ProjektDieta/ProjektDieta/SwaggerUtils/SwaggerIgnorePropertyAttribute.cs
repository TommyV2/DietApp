using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektDieta.SwaggerUtils
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerIgnorePropertyAttribute : Attribute
    {
    }
}
