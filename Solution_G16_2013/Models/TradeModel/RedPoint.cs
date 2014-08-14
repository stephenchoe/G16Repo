using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.TradeModel
{
  public  class RedPointGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RedPointValue { get; set; }
        //public int CompanyId { get; set; }
        //public Company Company { get; set; }

       // public double ChargeFeeLimit { get; set; }

        public virtual ICollection<ChargeFeeGroup> ChargeFeeGroups { get; set; }
        
    }

  public class RedPointRecord
  {
      public int Id { get; set; }
      public DateTime DateOfRecord { get; set; }
      public bool IsIncome { get; set; }
      public int RedPoints { get; set; }
      public string Item { get; set; }
      public string Remark { get; set; }
      public int Balance { get; set; }
  }
 


}
