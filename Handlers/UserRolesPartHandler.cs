using System.Linq;
using System.Collections.Generic;
using Orchard.Data;
using Orchard.ContentManagement.Handlers;
using Orchard.Roles.Models;

namespace Orchard.Roles.Handlers {
    public class UserRolesPartHandler : ContentHandler {
        private readonly IRepository<UserRolesPartRecord> _userRolesRepository;

        public UserRolesPartHandler(IRepository<UserRolesPartRecord> userRolesRepository) {
            _userRolesRepository = userRolesRepository;

            Filters.Add(new ActivatingFilter<UserRolesPart>("Company"));
            
        }
    }
}