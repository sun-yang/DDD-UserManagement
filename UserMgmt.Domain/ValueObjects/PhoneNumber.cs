using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMgmt.Domain.ValueObjects
{
    public record PhoneNumber(int RegionCode, string Number);

}
