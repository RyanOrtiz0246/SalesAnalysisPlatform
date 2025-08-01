using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAnalysisPlatform.Infrastructure.Exceptions
{
    public class SaleException : Exception
    {
        public SaleException(string message) : base(message) { }
    }
}
