using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StagingWizard.UIContracts.GitElements
{
    public class Participant
    {
        public string name { get; set; }
        public string email { get; set; }
        public DateTime date { get; set; }
    }
}
