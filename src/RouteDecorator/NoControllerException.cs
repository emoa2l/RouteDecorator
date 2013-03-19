using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteDecorator
{
    public class NoControllerException : Exception
    {
        public NoControllerException() 
            : base("No controller defined and unable to infer from usage.")
        {
            
        }
    }
}
