using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CIT275_Back_end_interface.Models
{
    public enum DeploymentStatusEnum {
        Unknown=0,
        Deployed=1,
        ReadyToDeploy=2,
        InhouseMaintenance=3,
        OutsideRepair=4,
        Reserved=5,
        FutureUse=6,
        Retired=86
    }

    public enum AssetState {
        Unknown=0,
        Active=1,
        InTransit=2,
        Staged=3,
        InTesting=4,
        OutOfCommission=5,
        RemovedFromInventory=86
        
    }

    public class Asset
    {
        [Key]
        public int AssetID { get; set; }
        [DefaultValue(0)]
        public int AssetType { get; set; }

        public string IdentNbr { get; set; }
        [StringLength(50)]
        [Required]
        public string Name  { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

        public DateTime CommissionedDt { get; set; }
        public DateTime DecommissionedDt { get; set; }

        [DefaultValue(0)]
        public DeploymentStatusEnum DeploymentStatus { get; set; } //--deployed, awaiting deployment, being repaired, 
        [DefaultValue(0)]
        public AssetState State { get; set; }  //--active, down for maint, offline etc
        public DateTime LastMaintDt { get; set; }
        public bool DeleteInd { get; set; }


    }
}