using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CIT275_Back_end_interface.Models
{
    public class Client
    {
       

        public enum IAPhoneType {
            None=0,
            Main=1,
            Direct=2,
            Work=3,
            Home=4,
            Primary_Contact=5,
            Fax=6,
            Cell=7,
            Video=8,
            Msg=9,
            Pager=10,
            Text=11,
            Other=12
        }

        [Key]
        public int ClientID { get; set; }

        [Display(Name = "Company Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Company Name is Required Field.")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "Company Name Length Between 4 to 200 character")]
        public string CompanyName { get; set; }

        [Display(Name = "Address")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "Address Length Between 4 to 200 character")]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        [StringLength(200)]
        public string Address2 { get; set; }

        [StringLength(50, MinimumLength = 4, ErrorMessage = "City Length Between 4 to 200 character")]
        public string City { get; set; }

        [Display(Name = "State")]
        [StringLength(50)]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        [StringLength(20)]
        public string ZipCode { get; set; }

        [Display(Name = "Phone")]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone Number is Required Field.")]
        public string Phone1 { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Phone Type")]
        public IAPhoneType? Phone1Type { get; set; }


        [Display(Name = "Phone")]
        [StringLength(50)]
        public string Phone2 { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Phone Type")]
        public IAPhoneType? Phone2Type { get; set; }

        //[RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "Email Address is not Valid.")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Country")]
        [StringLength(50)]
        public string Country { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Eff. Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EffDate { get; set; }
     
        [DefaultValue(0)]
        public bool? Active { get; set; }
        [DefaultValue(0)]
        public bool? DeleteInd { get; set; }




    }
}