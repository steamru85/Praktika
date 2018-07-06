using System.Collections.Generic;

namespace StagingWizard.UIContracts.GitElements
{
    public class CommitInfo
    {
        public string url { get; set; }
        public string sha { get; set; }
        public string node_id { get; set; }
        public string html_url { get; set; }
        public string comments_url { get; set; }
        public Commit commit { get; set; }
        public Participant author { get; set; }
        public Participant commiter { get; set; }
        public List<Commit> parents { get; set; }
        public Stats stats { get; set; }
        public List<File> files { get; set; }
    }
}
