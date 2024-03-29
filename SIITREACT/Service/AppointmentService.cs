using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SIITREACT.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;


namespace SIITREACT.Service
{
    public class AppointmentService
    {
        private readonly MyDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserService userService;

        public AppointmentService(MyDbContext dbContext, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        //Creare appointment
        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {

            var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (currentUser == null)
            {
                throw new ApplicationException("User not authenticated");
            }

            appointment.UserId = currentUser.Id;

            _dbContext.Appointments.Add(appointment);
            await _dbContext.SaveChangesAsync();

            return appointment;
        }

        //De facut vizualizare,stergere, editare programari
    }
}