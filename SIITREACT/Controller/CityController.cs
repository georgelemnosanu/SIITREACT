using Microsoft.AspNetCore.Mvc;
using SIITREACT.Model;

namespace SIITREACT.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CityController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<City> GetCities()
        {
            return _context.City.ToList();
        }
    }
}
