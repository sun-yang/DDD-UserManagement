using UserMgmt.Domain.ValueObjects;

namespace UserMgmt.WebAPI.Controllers
{
    public record LoginByPhonePasswordRequest(PhoneNumber phoneNumber, string passWord);
   
}
