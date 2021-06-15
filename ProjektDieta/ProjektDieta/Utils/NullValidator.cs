using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjektDieta.Utils
{
    public static class NullValidator<T>
    {
        public static bool ValidateAll(T obj)
        {
            var properties = obj.GetType().GetProperties();
            foreach(var p in properties)
            {
                if (p.GetValue(obj) == null)
                    return false;
            }
            return true;
        }

        public static bool ValidateFor(T obj, params Func<T, object>[] properties)
        {
            foreach(var p in properties)
            {
                if (p(obj) == null)
                    return false;
            }
            return true;
        }
    }
}
