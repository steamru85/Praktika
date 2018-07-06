using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StagingWizard.UIContracts.GitElements
{
    public class Verification
    {
        public bool verified { get; set; }
        public string reason { get; set; }
        public object signature { get; set; }
        public object payload { get; set; }
    }
}
