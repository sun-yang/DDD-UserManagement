using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgmt.Domain;
using UserMgmt.Domain.ValueObjects;

namespace UserMgmt.Infrastructure
{
    public class MockSmsSender : ISmsCodeSender
    {
        public Task SendSmsCodeAsync(PhoneNumber phoneNumber, string code)
        {
            Console.WriteLine($"Send code {code} to {phoneNumber.RegionCode}_{phoneNumber.Number}");
            return Task.CompletedTask;
        }
    }
}
