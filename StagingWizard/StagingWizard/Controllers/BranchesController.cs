using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StagingWizard.DataLayerContracts;
using StagingWizard.UIContracts;

namespace StagingWizard.Controllers
{
    [Produces("application/json")]
    [Route("api/branches")]
    public class BranchesController : Controller
    {
        public IBranchesRepository BranchesRepository { get; }

        public BranchesController(IBranchesRepository branchesRepository)
        {
            BranchesRepository = branchesRepository;
        }

        [Route("list")]
        [HttpPost]
        public List<string> GetBranches()
        {
            return BranchesRepository.GetBranches();
        }

        [Route("find")]
        [HttpPost]
        public List<string> FindBranches([FromBody]SearchKey searchKey)
        {
            return BranchesRepository.FindBranches(searchKey);
        }
    }
}