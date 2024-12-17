using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using SiteAsientos.ViewModels;

namespace SiteAsientos.Services
{
    public interface ILoginService
    {
        public Task<bool> Login(LoginVM modelo, HttpContext context);

        public Task<bool> SignOut(HttpContext context);
    }
}
