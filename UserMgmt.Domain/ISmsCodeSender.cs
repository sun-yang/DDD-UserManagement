using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgmt.Domain.ValueObjects;

namespace UserMgmt.Domain
{
    public interface ISmsCodeSender
    {
        public Task SendSmsCodeAsync(PhoneNumber phoneNumber, string code);
    }
}
