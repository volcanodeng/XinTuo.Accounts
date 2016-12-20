using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Logging;
using Orchard.Security;
using Orchard.Users.Events;
using Orchard;

namespace XinTuo.Accounts.EventHandlers
{
    public class UserEventHandler : Component, IUserEventHandler
    {
        private readonly IWorkContextAccessor _context;

        public UserEventHandler(IWorkContextAccessor context)
        {
            _context = context;
        }

        public void AccessDenied(IUser user)
        {
            
        }

        public void Approved(IUser user)
        {
            
        }

        public void ChangedPassword(IUser user)
        {
            
        }

        public void ConfirmedEmail(IUser user)
        {
            
        }

        public void Created(UserContext context)
        {
            
        }

        public void Creating(UserContext context)
        {
            
        }

        public void LoggedIn(IUser user)
        {
            
        }

        public void LoggedOut(IUser user)
        {
            _context.GetContext().HttpContext.Session.Abandon();
        }

        public void LoggingIn(string userNameOrEmail, string password)
        {
            
        }

        public void LogInFailed(string userNameOrEmail, string password)
        {
            
        }

        public void SentChallengeEmail(IUser user)
        {
            
        }
    }
}