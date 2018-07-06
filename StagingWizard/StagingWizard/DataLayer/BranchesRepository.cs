using System.Collections.Generic;
using StagingWizard.DataLayerContracts;
using StagingWizard.UIContracts;
using StagingWizard.UIContracts.GitElements;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace StagingWizard.DataLayer
{
    public class BranchesRepository : IBranchesRepository
    {
        public BranchesRepository() { }

        private const string GitHubAPI = "https://api.github.com";

        private HttpWebResponse GetResponse(string query, string method)
        {
            string url = GitHubAPI + query;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.Accept = "application/vnd.github.v3+json";
            request.UserAgent = "Code Sample Web Client";

            return (HttpWebResponse)request.GetResponse();
        }

        private List<Branch> GetBranchesList(Stream stream)
        {
            var reader = new StreamReader(stream, Encoding.UTF8);
            var result = reader.ReadToEnd();
            List<Branch> branches = (JsonConvert.DeserializeObject<List<Branch>>(result));

            return branches;
        }

        private List<string> GetBranchesNames(List<Branch> branches)
        {
            List<string> list = new List<string>();
            foreach (Branch b in branches)
                list.Add(b.Name);
            return list;
        }

        public List<string> GetBranches()
        {
            HttpWebResponse response = GetResponse("/repos/steamru85/Praktika/branches", "GET");
            List<Branch> branches = GetBranchesList(response.GetResponseStream());
            return GetBranchesNames(branches);
        }

        public Commit GetLastCommit(Branch branch)
        {
            string sha = branch.commit.sha;
            HttpWebResponse response = GetResponse("/repos/steamru85/Praktika/commits/" + sha, "GET");

            var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            var result = reader.ReadToEnd();

            CommitInfo lastCommit = (JsonConvert.DeserializeObject<CommitInfo>(result));
            return lastCommit.commit;
        }

        private List<Branch> SortBranches(List<Branch> branches)
        {
            foreach (Branch b in branches)
                b.commit = GetLastCommit(b);

            branches.Sort(delegate (Branch b1, Branch b2)
            { return -b1.commit.author.date.CompareTo(b2.commit.author.date); });

            return branches;
        }

        private List<Branch> SelectFoundBranches(List<Branch> branches)
        {
            List<Branch> sortedData = SortBranches(branches);
            if (sortedData.Count > 10)
                return sortedData.GetRange(0, 10);
            return sortedData;
        }

        public List<string> FindBranches(SearchKey searchKey)
        {
            HttpWebResponse response = GetResponse("/repos/steamru85/Praktika/branches", "GET");
            List<Branch> allBranches = GetBranchesList(response.GetResponseStream());

            List<Branch> foundData = new List<Branch>();
            foreach (Branch b in allBranches)
                if (b.Name.Contains(searchKey.KeyString))
                    foundData.Add(b);

            foundData = SelectFoundBranches(foundData);
            List<string> list = new List<string>();
            foreach (Branch b in foundData)
                list.Add(b.Name);
            return list;
        }
    }
}
