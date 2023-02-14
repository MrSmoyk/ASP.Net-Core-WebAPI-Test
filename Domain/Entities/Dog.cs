using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Dog : BaseEntity
    {
        public string Color { get; set; }

        public int TailLength { get; set; }

        public int Weight { get; set; }
    }
}
