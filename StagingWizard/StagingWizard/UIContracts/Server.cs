using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StagingWizard.UIContracts
{
    public class Server
    {
        public Guid Id { get; set; }
        public string IP { get; set; }
        public Dictionary<string, string> Services { get; set; }
        public string ServerRole { get; set; }
        public DateTime? Created { get; set; }
    }
}
