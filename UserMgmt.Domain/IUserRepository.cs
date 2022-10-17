using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgmt.Domain.Entities;
using UserMgmt.Domain.ValueObjects;

namespace UserMgmt.Domain
{
    public interface IUserRepository
    {
        public Task<User?> FindOneAsync(Guid userId);
        public Task<User?> FindOneAsync(PhoneNumber phoneNumber);
        public Task AddNewLoginHistoryAsync(PhoneNumber phoneNumber, string message);
        public Task SavePhoneCodeAsync(PhoneNumber phoneNumber, string code);
        public Task<string?> RetrievePhoneCodeAsync(PhoneNumber phoneNumber);
        public Task PublishEnventAsync(UserAccessResultEvent _event);
    }
}
