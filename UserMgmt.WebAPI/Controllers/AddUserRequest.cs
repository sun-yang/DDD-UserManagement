using UserMgmt.Domain.ValueObjects;

namespace UserMgmt.WebAPI.Controllers
{
    public record AddUserRequest(PhoneNumber phoneNumber, string passWord);
   
}
