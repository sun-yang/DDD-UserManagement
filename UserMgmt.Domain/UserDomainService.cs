using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgmt.Domain.Entities;
using UserMgmt.Domain.ValueObjects;

namespace UserMgmt.Domain
{
    public class UserDomainService
    {
        private readonly IUserRepository userRepository;
        private readonly ISmsCodeSender smsSender;

        public UserDomainService(IUserRepository userRepository, ISmsCodeSender smsSender)
        {
            this.userRepository = userRepository;
            this.smsSender = smsSender;
        }

        public void ResetAccessFail(User user)
        {
            user.UserAccessFail.Reset();
        }
        public bool IsLockedOut(User user)
        {
            return user.UserAccessFail.IsLockedOut();
        }
        public void AccessFail(User user)
        {
            user.UserAccessFail.Fail();
        }
        public async Task<UserAccessResult> CheckPassword(PhoneNumber phoneNumber, string password)
        {
            UserAccessResult result;
            var user = await userRepository.FindOneAsync(phoneNumber);
            if (user == null)            
                result = UserAccessResult.PhoneNumberNotFound;            
            else if (IsLockedOut(user))            
                result = UserAccessResult.LockOut;            
            else if (!user.HasPassWord())
                result = UserAccessResult.NoPassword;
            else if (user.CheckPassword(password))
                result = UserAccessResult.OK;
            else
                result = UserAccessResult.PasswordError;

            if (result == UserAccessResult.OK)
                ResetAccessFail(user);
            else
                AccessFail(user);

            await userRepository.PublishEnventAsync(new UserAccessResultEvent(phoneNumber, result));
           
            return result;
        }

        public async Task<CheckCodeResult> CheckPhoneNumberCodeAsync(PhoneNumber phoneNumber, string code)
        {            
            var user = await userRepository.FindOneAsync(phoneNumber);
            if(user == null)
               return CheckCodeResult.PhoneNumberNotFound;
            if(IsLockedOut(user))
                return CheckCodeResult.LockOut;

            string? codeInServer = await userRepository.RetrievePhoneCodeAsync(phoneNumber);
            if (string.IsNullOrEmpty(codeInServer))
                return CheckCodeResult.CodeError;
            if (codeInServer == code)
                return CheckCodeResult.OK;
            else
            {
                AccessFail(user);
                return CheckCodeResult.CodeError;
            }               

        }
       
    }
}
