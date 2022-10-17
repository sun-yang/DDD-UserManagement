using Microsoft.AspNetCore.Mvc;
using UserMgmt.Domain;
using UserMgmt.Infrastructure;

namespace UserMgmt.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserDomainService userService;

        public LoginController(UserDomainService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [UnitOfWorkAtrribute(typeof(UserDbContext))]//checkpassword may need change database, so need call the filter to savechanges
        public async Task<IActionResult> LoginByPhoneAndPassword(LoginByPhonePasswordRequest request)
        {
            if(request.passWord.Length < 6)
            {
                return BadRequest("Password must be no less than 6 characters.");
            }
            var result = await userService.CheckPassword(request.phoneNumber, request.passWord);
            switch(result)
            {
                case UserAccessResult.OK:
                    return Ok("Login succeed.");
                case UserAccessResult.PasswordError:
                case UserAccessResult.NoPassword:
                case UserAccessResult.PhoneNumberNotFound:
                    return BadRequest("Login failed.");
                case UserAccessResult.LockOut:
                    return BadRequest("Account is locked out.");
                default:
                    throw new ApplicationException($"Unknow value {result}.");                  
            }
        }        
    }
}
