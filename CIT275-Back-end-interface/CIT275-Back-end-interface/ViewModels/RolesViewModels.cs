using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIT275_Back_end_interface;
namespace ViewModels
{
    public class RoleNameVM
    {
        public string id { get; set; }
        public string Name { get; set; }
    }

    public class UserRoleVM
    {

        public string Email { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }

    }

    public class UserList
    {

        public string Email { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        [Key]
        public string UserId { get; set; }

    }
    public class RolesForUser
    {

        public bool HasRole { get; set; }
        [Key]

        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string UserId { get; set; }
        public string Disabled { get; set; }
    }

}
