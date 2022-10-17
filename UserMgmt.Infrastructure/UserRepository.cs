using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgmt.Domain;
using UserMgmt.Domain.Entities;
using UserMgmt.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using MediatR;

namespace UserMgmt.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext dbContext;
        private readonly IDistributedCache distributedCache;
        private readonly IMediator mediator;

        public UserRepository(UserDbContext dbContext, IMediator mediator, IDistributedCache distributedCache)
        {
            this.dbContext = dbContext;
            this.mediator = mediator;
            this.distributedCache = distributedCache;
        }

        public async Task AddNewLoginHistoryAsync(PhoneNumber phoneNumber, string message)
        {
            User? user = await FindOneAsync(phoneNumber);
            if(user != null)
                dbContext.UserLoginHistories.Add(new UserLoginHistory(user.Id, phoneNumber, message));
            
        }

        public async Task<User?> FindOneAsync(Guid userId)
        {
            User? user = await dbContext.Users.Include(u => u.UserAccessFail).SingleOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task<User?> FindOneAsync(PhoneNumber phoneNumber)
        {
            User? user = await dbContext.Users.Include(u => u.UserAccessFail).SingleOrDefaultAsync(u => u.PhoneNumber.Number == phoneNumber.Number &&
            u.PhoneNumber.RegionCode == phoneNumber.RegionCode);

            return user;
        }

        public Task PublishEnventAsync(UserAccessResultEvent _event)
        {
            return mediator.Publish(_event);
        }

        public async Task<string?> RetrievePhoneCodeAsync(PhoneNumber phoneNumber)
        {
            string key = $"PhoneCode_{phoneNumber.RegionCode}_{phoneNumber.Number}";
            string? code = await distributedCache.GetStringAsync(key);
            distributedCache.Remove(key);
            return code;
        }

        public Task SavePhoneCodeAsync(PhoneNumber phoneNumber, string code)
        {
            string key = $"PhoneCode_{phoneNumber.RegionCode}_{phoneNumber.Number}";
            return distributedCache.SetStringAsync(key, code, 
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });
        }
    }
}
