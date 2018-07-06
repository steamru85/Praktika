using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StagingWizard.UIContracts.GitElements
{
    public class Commit
    {
        public string sha { get; set; }
        public string url { get; set; }
        public Participant author { get; set; }
        public Participant commiter { get; set; }
        public string message { get; set; }
        public Tree tree { get; set; }
        public List<Commit> parents { get; set; }
        public Verification verification { get; set; }
    }
}
