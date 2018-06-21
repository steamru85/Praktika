using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StagingWizard.UIContracts
{
    public class StagingInLayer : StagingInList
    {
        public IEnumerable<Server> Servers { get; set; }
        public string InputParams { get; set; }
    }
}
