using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StagingWizard.UIContracts
{
    public class StagingInList
    {
        public Guid Id { get; set; }
        public string Creator { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastUpdated { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public EStagingState State { get; set; }
        public string CurrentStep { get; set; }
    }
}
