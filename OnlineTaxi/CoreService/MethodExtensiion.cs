using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService
{
    public static class MethodExtensiion
    {
        public static long ToLong(this object o)
        {
            try
            {
                return (long)o;
            }
            catch
            {
                return 0;
            }
        }

        public static long? ToNullableLong(this object o)
        {
            try
            {
                return (long)o;
            }
            catch
            {
                return null;
            }
        }
    }
}
