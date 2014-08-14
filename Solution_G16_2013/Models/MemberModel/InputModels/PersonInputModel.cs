using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;



namespace G16_2013.Models.MemberModel
{
    public class BaseInput
    {
        CurrentMode currentMode;
        public CurrentMode CurrentMode
        {
            get { return currentMode; }
            set { currentMode = value; }
        }
        public string InputTitle { get; set; }
        public string ActionName { get; set; }
        public string CotrollerName { get; set; }
        public string AppendUrl { get; set; }

        public string CancelRedirectUrl { get; set; }
        public string CancelActionName { get; set; }
        public string CancelCotrollerName { get; set; }
        public string CancelAppendUrl { get; set; }
        public string ParentDivId { get; set; }

    }
    public enum CurrentMode
    {
        ReadOnly,
        Insert,
        Edit
    }

     [MetadataType(typeof(PersonInputModel))]
    public class PersonInputModel : BaseInput
    {
        public int PersonId { get; set; }
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "請填寫姓名")]
        public string Name { get; set; }
        [Display(Name = "身分證號")]
        public string TWID { get; set; }
        [Display(Name = "性別")]
        public bool Gender { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "生日")]
        public DateTime? Birthday { get; set; }
        public List<BaseOption> IdentityOptions { get; set; }
        public List<int> IdentityIds { get; set; }
        [Display(Name = "備註")]
        public string PS { get; set; }
        public bool IsPersonExist { get; set; }
        public bool AllowSelect { get; set; }
        public int SelectedPersonId { get; set; }

        public virtual PersonContactInfoInputModel ContactInfoInput { get; set; }

    }
    public class PersonViewModel : PersonInputModel
    {
        public bool AllowEdit { get; set; }
        public string GenderText { get; set; }
        public PersonContactInfoViewModel ContactInfoView { get; set; }

    }

    public class PersonContactInfoInputModel : BaseInput
    {
        public int Id { get; set; }
        [Display(Name = "手機")]
        public string Phone { get; set; }
        [Display(Name = "市話")]
        public string TEL { get; set; }
        public string Email { get; set; }

        public AddressInputModel AddressInput { get; set; }

        public bool PersonExist { get; set; }
        public bool AllowEdit { get; set; }

    }
    public class PersonContactInfoViewModel : PersonContactInfoInputModel
    {
        //public string ZipText { get; set; }
        //public string CityText { get; set; }
        //public string DistrictText { get; set; }
        public AddressViewModel AddressView { get; set; }
    }

    public class PersonSelectCreateInput
    {
        public bool PersonExsit { get; set; }
        public int ExsitPersonId { get; set; }
        public PersonInputModel NewPersonInput { get; set; }
        public PersonViewModel ExistPersonView { get; set; }
    }

    public class AddressInputModel
    {
        public string ZipCode { get; set; }
        public string CityId { get; set; }
        public string DistrictId { get; set; }
        public string Street { get; set; }
        public List<BaseOption> CityOptions { get; set; }
        public List<BaseOption> DistrictOptions { get; set; }
    }
    public class AddressViewModel : AddressInputModel
    {
        public string CityText { get; set; }
        public string DistrictText { get; set; }
    }

}
