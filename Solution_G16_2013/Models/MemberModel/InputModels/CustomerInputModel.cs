using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.MemberModel
{
    public abstract class CustomerInputModel
    {
        public PersonSelectCreateInput PersonSelectCreateInput { get; set; }      
     
    }

    public class FuturesCustomerInput : CustomerInputModel
    {
        public FuturesAccountInputModel FuturesAccountInput { get; set; }
      
    }
    public class StockCustomerInput : CustomerInputModel
    {
        public StockAccountInputModel StockAccountInput { get; set; }
    
    }
}
