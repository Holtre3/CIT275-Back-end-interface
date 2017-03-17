using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CIT275_Back_end_interface.Models;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using ViewModels;
namespace DAL
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DataRepository
    {
        ApplicationDbContext _dc = new ApplicationDbContext();
    }
}