using api.Extensions;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userId = resultContext.HttpContext.User.GetUsername();
            var uow = resultContext.HttpContext.RequestServices.GetService<IUnitOfWork>();
            var user = await uow.UserRepository.GetUserByUsernameAsync(userId);
            user.LastActive = DateTime.Now;
            await uow.Complete();
        }
    }
}
