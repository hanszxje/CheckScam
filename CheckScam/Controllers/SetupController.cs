using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CheckScam.Controllers
{
    public class SetupController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public SetupController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> CreateSuperuserRole()
        {
            if (!await _roleManager.RoleExistsAsync("Superuser"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Superuser"));
            }
            return Content("Superuser role created.");
        }
    }
}