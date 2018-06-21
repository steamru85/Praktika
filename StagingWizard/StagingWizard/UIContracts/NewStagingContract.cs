using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StagingWizard.UIContracts
{
    public class NewStagingContract
    {
        public BackEndStagingContract BackEnd { get; set; }
        public FrontEndStagingContract Front { get; set; }
        public CRMStagingContract CRM { get; set; }
        public ABSStagingContract ABS { get; set; }
    }

    public class BackEndStagingContract
    {
        public string BranchName { get; set; }
        public bool OnlyDb { get; set; }
    }

    public class FrontEndStagingContract
    {
        public string BranchName { get; set; }
    }

    public class CRMStagingContract
    {

    }

    public class ABSStagingContract
    {

    }

}
