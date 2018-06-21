using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;


namespace StagingWizard.UIContracts
{
    public class UpdateStagingContract
    {
        public Guid Id { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public EStagingState State { get; set; }
        public string CurrentStep { get; set; }

    }
}
