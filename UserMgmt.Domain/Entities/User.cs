using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgmt.Domain.ValueObjects;
using Zack.Commons;

namespace UserMgmt.Domain.Entities
{
    public record User : IAggregateRoot
    {
        public Guid Id { get; init; }
        public PhoneNumber PhoneNumber { get; private set; }
        private string? PasswordHash;
        public UserAccessFail UserAccessFail { get; private set; }
        private User() { }
        public User(PhoneNumber phoneNumber)
        {
            this.Id = new Guid();
            this.PhoneNumber = phoneNumber;
            this.UserAccessFail = new UserAccessFail(this);
        }
        public void ChangePassword(string newPassword)
        {
            if(newPassword.Length <= 6)
            {
                throw new ArgumentException("Password can't be less than 6 characters.");
            }
            PasswordHash = HashHelper.ComputeMd5Hash(newPassword);
        }

        public bool HasPassWord()
        {
            return !string.IsNullOrEmpty(PasswordHash);
        }

        public bool CheckPassword(string password)
        {
            return PasswordHash == HashHelper.ComputeMd5Hash(password);
        }

        public void ChangePhoneNumber(PhoneNumber phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
        }

    }
}
