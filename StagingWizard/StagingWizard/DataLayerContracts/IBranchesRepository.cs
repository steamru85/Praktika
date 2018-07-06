using StagingWizard.UIContracts;
using System.Collections.Generic;

namespace StagingWizard.DataLayerContracts
{
    public interface IBranchesRepository
    {
        List<string> GetBranches();
        List<string> FindBranches(SearchKey searchey);
    }
}
