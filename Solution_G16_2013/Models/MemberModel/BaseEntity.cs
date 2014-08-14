using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.MemberModel
{
   public abstract class BaseEntity
    {
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public bool Removed { get; set; }

        
    }

   public class StatusAlert
   {
       public string Status { get; set; }
       public string PositionId { get; set; }
       public int Timeout { get; set; }
   }
}
