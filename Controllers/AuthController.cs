using Custom_Identity_Auth.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net.Http;
using Supabase;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Custom_Identity_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       

        // GET: api/<AuthController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Hello", "World" };
        }


        // Fix for CS1519 and CS8124
        [HttpPost]
        public async Task<Results<Ok, ValidationProblem>> CreateNewUser([FromBody] RegistrationModel registration)
        {
            var userManager = HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
            //var userManager = new ApplicationUserStore(_httpClient, _supabase);
            //var userStore = HttpContext.RequestServices.GetRequiredService<IUserStore<ApplicationUser>>();

            var user = new ApplicationUser
            {
                UserName = registration.IdNo+registration.Email,
                Email = registration.Email,
                IdNo=registration.IdNo
            };

            var result = await userManager.CreateAsync(user, registration.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.ToDictionary(e => e.Code, e => new[] { e.Description });

                return TypedResults.ValidationProblem(errors);
            }

            return TypedResults.Ok();
        }


        //// GET api/<AuthController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// PUT api/<AuthController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<AuthController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}


    }
}
