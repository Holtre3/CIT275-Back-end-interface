using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace CIT275_Back_end_interface.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        //Additional Properties

        //[RegularExpression(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$", ErrorMessage = "Email Address is not Valid.")]

        public int UserType { get; set; }
        [Display(Name = "Choose Company:")]
        public int ClientID { get; set; }
        [Display(Name = "I am a new client:")]
        public bool NewClient { get; set; }
        //       [StringLength(200, MinimumLength = 4, ErrorMessage = "Company Name Length Between 4 to 200 character")]
        [StringLength(200)]
        [Display(Name = "Company Name:")]
        public string CompanyName { get; set; }

        [StringLength(50)]
        [Display(Name = "Contact Phone:")]
        public string Phone { get; set; }

        [StringLength(50)]
        [Display(Name = "Best Time to Call:")]
        public string ContactTime { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50)]
        public string LastName { get; set; }


        [Display(Name = "First Name")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        [MaxLength(50)]
        public string City { get; set; }

        [Display(Name = "State")]
        [MaxLength(50)]
        public string State { get; set; }

        [Display(Name = "Zip")]
        [MaxLength(10)]
        public string Zip { get; set; }

        //TODO: publish to azure


    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
           // : base("DefaultConnection", throwIfV1Schema: false)
            : base("DEV", throwIfV1Schema: false)
           // :  base("PROD", throwIfV1Schema: false)
         {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<ClientAsset> ClientAssets {get; set;}
        public DbSet<FileLog> FileLogs { get; set; }
        public DbSet<TransactionLog> TransactionLogs { get; set; }

    }
}