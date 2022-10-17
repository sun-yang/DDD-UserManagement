using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMgmt.Domain
{
    public enum UserAccessResult
    {
        OK, PhoneNumberNotFound, LockOut, NoPassword, PasswordError
    }
}
