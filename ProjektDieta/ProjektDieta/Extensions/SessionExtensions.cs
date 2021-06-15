using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProjektDieta.Extensions
{
    public static class SessionExtensions
    {
        public static void SetLong(this ISession session, string key, long value)
        {
            session.SetString(key, value.ToString());
        }

        public static long? GetLong(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : Int64.Parse(value);
        }
    }
}
