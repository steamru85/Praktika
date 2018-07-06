using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StagingWizard.UIContracts.GitElements
{
    public class File
    {
        public string name { get; set; }
        public int additions { get; set; }
        public int deletions { get; set; }
        public int changes { get; set; }
        public string status { get; set; }
        public string raw_url { get; set; }
        public string blob_url { get; set; }
        public string patch { get; set; }
    }
}
