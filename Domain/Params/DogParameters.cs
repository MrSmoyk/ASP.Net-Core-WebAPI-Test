using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Params
{
    public class DogParameters : QueryStringParameters
    {
        public DogParameters()
        {
            OrderBy = "name desc";
        }
    }
}
