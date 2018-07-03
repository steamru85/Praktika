using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StagingWizard.UIContracts;

namespace StagingWizard.DataLayerContracts
{
    public interface IUserRepository
    {
        User SignInCheck(User user);
        User AddUser(User user);
        bool CheckToken(string token);
    }
}
