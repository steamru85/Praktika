using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StagingWizard.UIContracts.GitElements
{
    public class Branch
    {
        public string Name { get; set; }
        public Commit commit { get; set; }
    }
}
