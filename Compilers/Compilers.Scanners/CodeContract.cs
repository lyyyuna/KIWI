using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    static class CodeContract
    {
        public static void RequireArgumentNotNull(object argValue, string argName)
        {
            if (argValue == null)
            {
                throw new ArgumentNullException(argName);
            }
        }

        public static void RequireArgumentNotNull(object argValue, string argName, string message)
        {
            if (argValue == null)
            {
                throw new ArgumentNullException(argName, message);
            }
        }

        public static void RequireArgumentInRange(bool isInRange, string argName, string message)
        {
            if (!isInRange)
            {
                throw new ArgumentNullException(argName, message);
            }
        }

        public static void Require(bool condition, string argName)
        {
            if (!condition)
            {
                throw new ArgumentNullException(argName);
            }
        }

        public static void Require(bool condition, string argName, string message)
        {
            if (!condition)
            {
                throw new ArgumentNullException(argName, message);
            }
        }
    }
}
