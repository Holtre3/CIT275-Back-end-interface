using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CIT275_Back_end_interface.Models
{
    public enum CAStatus {
        Unknown=0,
        Active=1,
        Log_Overdue=2,
        Inactive=3,
        RMA_In_Transit =4,
        Down=5

    }

    public class ClientAsset
    {   
        public int ClientID { get; set; }
        public int AssetID { get; set; }
        public DateTime EffDt { get; set; }
        public DateTime EndDt { get; set; }
        [DefaultValue(0)]
        public CAStatus Status  { get; set; }
        public string LastLocation { get; set; } //GPS location?
        [Key]
        public int ClientAssetID { get; set; }

    }
}