
using Microsoft.AspNetCore.Identity;

namespace CMCS.Models
{
    public class User : IdentityUser
    {
        // You can add additional properties here if needed
        public string FullName { get; set; }
    }
}



