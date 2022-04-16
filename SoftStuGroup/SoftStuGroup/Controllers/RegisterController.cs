using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using SoftStuGroup.Models;


namespace SoftStuGroup.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : Controller
    {
        [HttpGet]
        public JsonResult Get()
        {
            return Json("Hello");
        }

        [HttpPost]
        public JsonResult Post(User user)
        {
            User u = new User();
            u.Username = user.Username;
            u.Password = user.Password;
            u.ConfirmPassword = user.ConfirmPassword;
            u.Img = user.Img;
            u.Role = false;

            return Json(u);

        }


    }
}
