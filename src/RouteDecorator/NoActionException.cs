using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteDecorator
{
    public class NoActionException : Exception
    {
        public NoActionException()
            : base("No action defined and unable to infer from usage.")
        {
            //just a  basic exception
        }
    }
}
