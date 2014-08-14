using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.TradeModel
{
   public class TradeRecord
    {
       public int Id { get; set; }
       public virtual TradeAccount Account { get; set; }
       public DateTime DateOfTrade { get; set; }
       public TimeSpan DealTime { get; set; }
       public int AccountId { get; set; }
    }

    
}
