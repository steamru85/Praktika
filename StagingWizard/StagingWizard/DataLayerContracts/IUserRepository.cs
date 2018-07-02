using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StagingWizard.UIContracts;

namespace StagingWizard.DataLayerContracts
{
    public interface IUserRepository
    {
        string SignInCheck(User user);
        string AddUser(User user);
        bool CheckToken(string token);
    }
}
