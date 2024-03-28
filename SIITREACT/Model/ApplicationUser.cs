using Microsoft.AspNetCore.Identity;

namespace SIITREACT.Model
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Appointment> Appointments { get; set; }

        public ApplicationUser()
        {
            Appointments = new List<Appointment>();
        }
    }
}