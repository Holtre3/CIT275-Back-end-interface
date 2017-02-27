using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CIT275_Back_end_interface.Models
{
    public enum FileStatus {
        NotSet=0,
        OK=1,
        Processing=2,
        Created=3,
        Error=99
    }

    public class FileLog
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "File Name is Required Field.")]
        public string FileName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "File Path is Required Field.")]
        public string FilePath { get; set; }
        public int ClientID { get; set; }
        public int AssetID { get; set; }
        [DefaultValue(0)]
        public FileStatus Status { get; set; }
        public bool DeleteInd { get; set; }
        public bool Archived { get; set; }
        public DateTime BaseDate { get; set; }   //For calculating event time
        public DateTime CreateDate { get; set; }

        public ICollection<TransactionLog> TranLogID { get; set; }
        [Key]
        public int ID { get; set; }  //BatchID

    }
}