using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SIITREACT.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SIITREACT.Service
{
    public class AppointmentService
    {
        private readonly MyDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppointmentService(MyDbContext dbContext, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserIdFromToken()
        {
            // Retrieve the JWT token from the request headers
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Decode the JWT token
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;

            // Extract the user ID from the JWT token's claims
            var userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

            return userId;
        }

        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {
            var userId = GetUserIdFromToken(); // Implement this method to extract user ID from JWT token
            if (userId == null)
            {
                throw new ApplicationException("User not authenticated");
            }

            // Associate the appointment with the user ID
            appointment.UserId = userId;

            // Save the appointment to the database
            _dbContext.Appointments.Add(appointment);
            await _dbContext.SaveChangesAsync();

            return appointment;
        }
    }
}