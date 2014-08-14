using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.MemberModel
{
    public class SearchInputModel
    {
        public string KeyWord { get; set; }
        public List<BaseOption> SearchOptions { get; set; }
        public int SearchWayId { get; set; }

        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }
    public class CompanySearchInput : SearchInputModel
    {
        public int ParentCompanyId { get; set; }
       public List<BaseOption> ParentCompanyOptions { get; set; }
    }

    public class CompanyAERecordSearchInput : SearchInputModel
    {
        public int CompanyId { get; set; }
      
    }
    public class CompanyAECodesSearchInput : SearchInputModel
    {
        public int CompanyId { get; set; }
        public int BusinessTypeId { get; set; }
        public List<BaseOption> BusinessTypeOptions { get; set; }

    }
  
}
