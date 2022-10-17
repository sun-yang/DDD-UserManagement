using System;

namespace UserMgmt.WebAPI
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UnitOfWorkAtrribute : Attribute
    {       
        public Type[] DbContextTypes { get; init; }

        public UnitOfWorkAtrribute(params Type[] dbContextTypes)
        {
            this.DbContextTypes = dbContextTypes;
        }
    }
}
