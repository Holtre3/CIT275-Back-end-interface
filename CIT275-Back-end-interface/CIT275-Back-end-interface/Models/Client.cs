using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CIT275_Back_end_interface.Models
{
    public class Client
    {
        [Key]
        public int ClientID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Company Name is Required Field.")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "Company Name Length Between 4 to 200 character")]
        public string CompanyName { get; set; }
        [StringLength(200, MinimumLength = 4, ErrorMessage = "Address Length Between 4 to 200 character")]
        public string Address1 { get; set; }
        [StringLength(200)]
        public string Address2 { get; set; }
        [StringLength(50, MinimumLength = 4, ErrorMessage = "City Length Between 4 to 200 character")]
        public string City { get; set; }

        [StringLength(5)]
        public string State { get; set; }
        [StringLength(20)]
        public string ZipCode { get; set; }
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone Number is Required Field.")]
        public string Phone1 { get; set; }
        public int Phone1Type { get; set; }
        [StringLength(50)]
        public string Phone2 { get; set; }
        public int Phone2Type { get; set; }
        [Required]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "Email Address is not Valid.")]
        public string Email { get; set; }
        

        public DateTime EffDate { get; set; }
        public bool Active { get; set; }

        public bool DeleteInd { get; set; }




    }
}