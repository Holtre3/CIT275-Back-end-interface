using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CIT275_Back_end_interface.Models
{
    public class TransactionLog
    {


        public int FileID { get; set; }
        [Required]
        public string MsgType { get; set; }

        public string Msg { get; set; }

        public int RowID { get; set; }              //store row number for sequencing
        public long EpocTime { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreateDate { get; set; }

        /*TODO:
         public override void Up() //put in seed
         {
             //AddColumn("dbo.DemoRecords", "CreateDate", c => c.DateTime());
             AddColumn("dbo.DemoRecords", "CreateDate", c => c.DateTime(defaultValueSql: "GETDATE()"));
         } 
        */

        [Key]
        public int ID { get; set; }

      
    }
}