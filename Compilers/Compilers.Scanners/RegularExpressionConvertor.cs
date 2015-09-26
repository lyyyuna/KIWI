using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    public abstract class RegularExpressionConvertor<T>
    {
        protected RegularExpressionConvertor()
        { }

        public virtual T Convert(RegularExpression expression)
        {
            if (expression == null)
            {
                return default(T);
            }

            switch (expression.ExpressionType)
            {
                case  RegularExpressionType.Empty:
                    return;
            }
            
        }


        public Convert
    }
}
