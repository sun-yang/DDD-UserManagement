using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserMgmt.Domain;
using UserMgmt.Domain.Entities;
using UserMgmt.Infrastructure;

namespace UserMgmt.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    ///CRUD can be put in domain service layer. 
    ///define CRUD functions in IUserRepository and define CRUD in UserDomainService
    ///But CRUD are not true business logic. We can use dbcontext directly. 
    ///This is also one of benefit of onion architecture
    public class CRUDController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly UserDbContext dbContext;

        public CRUDController(IUserRepository userRepository, UserDbContext dbContext)
        {
            this.userRepository = userRepository;
            this.dbContext = dbContext;
        }

        [HttpPost]
        [UnitOfWorkAtrribute(typeof(UserDbContext))]
        public async Task<IActionResult> AddNewUser(AddUserRequest req)
        {
            if(await userRepository.FindOneAsync(req.phoneNumber) != null)            
                return BadRequest("The phone number is used.");
           
            User user = new User(req.phoneNumber);
            user.ChangePassword(req.passWord);
            dbContext.Users.Add(user);           
            return Ok("User is added successfully.");
        }
    }
}
