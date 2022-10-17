using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMgmt.Domain.Entities
{
    public record UserAccessFail : IAggregateRoot
    {
        public Guid Id { get; init; }
        public User User { get; init; }
        public Guid UserId { get; init; }

        private bool isLockedOut;
        public DateTime? LockEnd { get; private set; }
        public int AccessFailCount { get; private set; }
        private UserAccessFail() { } // This is for EF core to use
        public UserAccessFail(User user)
        {
            this.Id = new Guid();
            this.User = user;
        }

        public void Reset()
        {
            this.isLockedOut = false;
            this.AccessFailCount = 0;
            this.LockEnd = null;
        }

        public void Fail()
        {
            this.AccessFailCount++;
            if (this.AccessFailCount >= 3)
            {
                this.LockEnd = DateTime.Now.AddMinutes(5);
                this.isLockedOut = true;
            }
        }
        public bool IsLockedOut()
        {
            if(this.isLockedOut)
            {
                if(this.LockEnd <= DateTime.Now)
                {
                    Reset();
                    return false;
                }
                else
                    return true;
            }
            else
                return false;
        }
    }
}
