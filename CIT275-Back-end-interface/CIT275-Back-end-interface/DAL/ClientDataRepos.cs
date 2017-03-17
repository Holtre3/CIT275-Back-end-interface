using CIT275_Back_end_interface.Models;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public partial class DataRepository
    {


        private class clID
        {
            public string UserID { get; set; }
            public int ClientID { get; set; }

        }

        public IEnumerable<Client> GetClientList(string company = "", string city = "", string state = "" ) {
            IEnumerable<Client> model ;


            string sql = "select * from Clients where ( CompanyName like  @company + '%'  or @company is null ) and ( City like  @city + '%'  or @city is null ) and ( State like  @state + '%'  or @state is null )  ";
            if (string.IsNullOrWhiteSpace(company)) {
                company = null;
            }
            if (string.IsNullOrWhiteSpace(city))
            {
                city = null;
            }
            if (string.IsNullOrWhiteSpace(state))
            {
                state = null;
            }

            var companyParam = new SqlParameter
            {
                ParameterName = "company",
                Value = company,
                IsNullable = true
            };

            var cityParam = new SqlParameter
            {
                ParameterName = "city",
                IsNullable = true,
                Value = city
            };
            var stateParam = new SqlParameter
            {
                ParameterName = "state",
                IsNullable = true,
                Value = state
            };
            model = _dc.Database.SqlQuery<Client>(sql, companyParam, cityParam, stateParam);

            return model.ToList();
        }

        public bool PutUserClientID(string UserID, int ClientID)
        {
            bool ret = false;
            int rc = 0;
            string sql = "update u set u.ClientID = @ClientID   from AspNetUsers u  where UserID = @UserID  ";

            var UserIDParam = new SqlParameter
            {
                ParameterName = "UserID",
                Value = UserID
            };

            var ClientIDParam = new SqlParameter
            {
                ParameterName = "ClientID",
                DbType = System.Data.DbType.Int32,
                Value = ClientID

            };
            rc = _dc.Database.ExecuteSqlCommand(sql, new { ClientID=ClientIDParam, UserID=UserIDParam  }); //.SqlQuery<clID>("Select top 1 UserID, ClientID, ContactID from ClientUsers where UserId = @uid", UserIDParam).SingleOrDefault();

            return ret;
        }


        public int GetClientByUID(string uid)
        {
            var idParam = new SqlParameter
            {
                ParameterName = "uid",
                Value = uid
            };
            clID rc = _dc.Database.SqlQuery<clID>("select top 1 ID as UserID, ClientID  from AspNetUsers u where u.Id = @uid", idParam).SingleOrDefault();
            return rc.ClientID;

        }
    }
}