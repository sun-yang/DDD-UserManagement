using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace UserMgmt.WebAPI
{
    public class UnitOfWorkFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var result = await next();
            if(result.Exception != null)//only savechanges to database when action is executed seccussfully
                return;

            var actionDesc = context.ActionDescriptor as ControllerActionDescriptor;
            if(actionDesc == null)
                return ;

            var uowAtrribute = actionDesc.MethodInfo.GetCustomAttributes<UnitOfWorkAtrribute>().FirstOrDefault();
            //var uowAtrribute = actionDesc.MethodInfo.GetCustomAttribute(typeof(UnitOfWorkAtrribute));

            if (uowAtrribute == null)
                return;
            
            foreach (var dbCtxTpye in uowAtrribute.DbContextTypes)
            {
                var dbCtx = context.HttpContext.RequestServices.GetService(dbCtxTpye) as DbContext; // get dbcontext object from DI
                if (dbCtx != null)
                   await dbCtx.SaveChangesAsync();
            }


        }
    }
}
