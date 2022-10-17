using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgmt.Domain.ValueObjects;

namespace UserMgmt.Domain.Entities
{
    public record UserLoginHistory : IAggregateRoot
    {
        public long Id { get; init; }
        public Guid? UserId { get; init; }//using id only to access another aggregate
        public PhoneNumber PhoneNumber { get; init; }
        public DateTime CreateTime { get; init; }
        public string Message { get; init; }
        private UserLoginHistory() { }
        public UserLoginHistory(Guid? userId, PhoneNumber phoneNumber, string message)
        {
            this.UserId = userId;
            this.CreateTime = DateTime.Now;
            this.PhoneNumber = phoneNumber;
            this.Message = message;
        }
    }
}
