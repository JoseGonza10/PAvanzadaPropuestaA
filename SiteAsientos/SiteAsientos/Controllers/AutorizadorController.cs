using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SiteAsientos.Models;
using SiteAsientos.Services.Interfaces;

namespace SiteAsientos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutorizadorController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly CubreasientosContext _context;

        public AutorizadorController(IJwtTokenService jwtTokenService, CubreasientosContext  context )
        {
            _jwtTokenService = jwtTokenService;

            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Employee loginRequest)
        {

            var employeeBD = await _context.Employee
                .FirstOrDefaultAsync(m => m.Employee_Email == loginRequest.Employee_Email);

            if (loginRequest == null)
            {
                ModelState.AddModelError("", "ID o contraseña incorrectos.");
                return BadRequest(loginRequest);
            }

            if (loginRequest.Employee_Email == employeeBD.Employee_Password)
            {
                var token = _jwtTokenService.GenerateToken(employeeBD.Employee_Id.ToString().Trim(), employeeBD.Employee_Name);
                return Ok(new { Token = token });
            }

            return Unauthorized("Usuario o contraseña incorrectos");
        }
    }
}
