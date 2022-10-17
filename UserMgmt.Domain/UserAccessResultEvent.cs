using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgmt.Domain.ValueObjects;

namespace UserMgmt.Domain
{
    public record UserAccessResultEvent(PhoneNumber phoneNumber, UserAccessResult accessResult) : INotification;
   
}
