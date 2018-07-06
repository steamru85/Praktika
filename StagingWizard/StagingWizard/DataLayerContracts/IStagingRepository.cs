using StagingWizard.UIContracts;
using System;
using System.Collections.Generic;

namespace StagingWizard.DataLayerContracts
{
    public interface IStagingRepository
    {
        IEnumerable<StagingInList> GetList();
        StagingInLayer GetStaging(Guid id);

        StagingInList GetStagingInList(Guid id);
        void CreateStaging(Guid id, string creator , EStagingState state, string currentstep, string inputParams);
        void UpdateStagingState(Guid id, EStagingState state, string currentstep);
        void AddServer(Guid stagingId, Server server);
        void DeleteStaging(Guid id);
    }
}
