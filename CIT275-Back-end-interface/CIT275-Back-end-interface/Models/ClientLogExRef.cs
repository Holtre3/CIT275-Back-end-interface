using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CIT275_Back_end_interface.Models
{
    public partial class ClientLogExRef
    {

        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int AssetID { get; set; }
        public FileStatus Status { get; set; }
        public bool DeleteInd { get; set; }
        public bool Archived { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime BaseDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CreateDate { get; set; }

        public int ClientID { get; set; }
        public string CompanyName { get; set; }

    }
}