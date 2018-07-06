using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StagingWizard.UIContracts.GitElements
{
    public class Stats
    {
        public int additions { get; set; }
        public int deletions { get; set; }
        public int total { get; set; }
    }
}
