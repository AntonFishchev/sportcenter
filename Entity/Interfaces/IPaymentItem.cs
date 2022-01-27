using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Abstract
{
    public interface IPaymentItem
    {
        public string Name { get; set; }
        public string TypeItem { get; set; }
        public double Price { get; set; }
    }
}
