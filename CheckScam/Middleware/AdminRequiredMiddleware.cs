using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CheckScam.Middleware
{
    public class AdminRequiredMiddleware
    {
        private readonly RequestDelegate _next;

        public AdminRequiredMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<IdentityUser> userManager)
        {
            if (context.Request.Path.StartsWithSegments("/admin"))
            {
                if (!context.User.Identity.IsAuthenticated)
                {
                    context.Response.Redirect("/Gr1/Login");
                    return;
                }

                var user = await userManager.GetUserAsync(context.User);
                if (!await userManager.IsInRoleAsync(user, "Superuser"))
                {
                    context.Response.Redirect("/Gr1/Index");
                    return;
                }
            }

            await _next(context);
        }
    }
}