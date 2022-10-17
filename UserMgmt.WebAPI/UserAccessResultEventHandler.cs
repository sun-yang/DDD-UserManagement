using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UserMgmt.Domain;
using UserMgmt.Infrastructure;

namespace UserMgmt.WebAPI
{
    public class UserAccessResultEventHandler : INotificationHandler<UserAccessResultEvent>
    {
        //private readonly UserRepository userRepository;
        //private readonly UserDbContext userDbContext;

        //public UserAccessResultEventHandler(UserRepository userRepository, UserDbContext userDbContext)
        //{
        //    this.userRepository = userRepository;
        //    this.userDbContext = userDbContext;
        //}

        private readonly IServiceScopeFactory serviceScopeFacotry;

        public UserAccessResultEventHandler(IServiceScopeFactory serviceScopeFacotry)
        {
            this.serviceScopeFacotry = serviceScopeFacotry;
        }

        public async Task Handle(UserAccessResultEvent notification, CancellationToken cancellationToken)
        {
            using var scope = serviceScopeFacotry.CreateScope();
            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            UserDbContext userDbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
            
            await userRepository.AddNewLoginHistoryAsync(notification.phoneNumber, $"Login {notification.accessResult}");
            await userDbContext.SaveChangesAsync();
        }
    }
}
