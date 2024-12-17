using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteAsientos.Models;
using SiteAsientos.ViewModels;
using System.Security.Claims;
using SiteAsientos.Extensions;
using Microsoft.AspNetCore.Authentication;

namespace SiteAsientos.Services
{
    public class LoginService : ILoginService
    {
        private readonly CubreasientosContext _context;

        public LoginService(CubreasientosContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<bool> Login(LoginVM modelo, HttpContext httpContext)
        {
            try
            { //Verificar la existencia del empleado
                var employee = await _context.Employee.FirstOrDefaultAsync(x => x.Employee_Email.Equals(modelo.Email) && x.Employee_Password == modelo.Password);
                if (employee != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,employee.Employee_Name),
                    new Claim("Email", employee.Employee_Email),
                };


                    //Agrega los roles a claims
                    foreach (string role in employee.Employee_Type)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }


                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    //Para utilizarlo dentro de la aplicacion
                    httpContext.Session.GuardarObjeto("user", employee);
                    return true;
                }
                return false;
            } catch (Exception E)
            {
                return false;
            }
        }

        public async Task<bool> SignOut(HttpContext context)
        {
            try
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }

        }
    }
}
