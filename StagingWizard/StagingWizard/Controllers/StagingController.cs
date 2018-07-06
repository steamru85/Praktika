using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StagingWizard.DataLayerContracts;
using StagingWizard.UIContracts;
using StagingWizard.Attributes;
using System.Net;
using System.Net.Http;
using System.IO;

namespace StagingWizard.Controllers
{
    [Produces("application/json")]
    [Route("api/staging")]
    public class StagingController : Controller
    {
        public IStagingRepository StagingRepository { get; }

        public StagingController(IStagingRepository stagingRepository)
        {
            StagingRepository = stagingRepository;
        }

        [Route("add")]
        [HttpPost, Authorization]
        public StagingInList Create(NewStagingContract staging)
        {
            Guid id = Guid.NewGuid();
            string inputParams = string.Empty;
            if (staging != null)
                inputParams = JsonConvert.SerializeObject(staging);
            StagingRepository.CreateStaging(id, "test", EStagingState.Success, "Создание стейджинга успешно завершено!", inputParams);
            StagingRepository.AddServer(id, new Server() { Created = DateTime.UtcNow, Id = Guid.NewGuid(), ServerRole = "Бэкенд API", IP = "172.21.13.77", Services = new Dictionary<string, string>() { { "URL", "http://172.21.13.77:8080" } } });
            StagingRepository.AddServer(id, new Server() { Created = DateTime.UtcNow, Id = Guid.NewGuid(), ServerRole = "Бэкенд БД", IP = "172.21.13.78", Services = new Dictionary<string, string>() { { "Postgresql", "172.21.13.78:5432" }, { "redis", "172.21.13.78:6543" }, { "rabbit", "172.21.13.78:5672" } } });
            StagingRepository.AddServer(id, new Server() { Created = DateTime.UtcNow, Id = Guid.NewGuid(), ServerRole = "Бэкенд асинк", IP = "172.21.13.79" });
            StagingRepository.AddServer(id, new Server() { Created = DateTime.UtcNow, Id = Guid.NewGuid(), ServerRole = "Фронтенд", IP = "172.21.13.80", Services = new Dictionary<string, string>() { { "URL", "http://172.21.13.80:8080" } } });
            return new StagingInList() { Created = DateTime.UtcNow, Creator = "test", CurrentStep = "Запускаем создание стейджинга", LastUpdated = DateTime.UtcNow, Id = id, State = EStagingState.InProcess };
        }

        [Route("update")]
        [HttpPost, Authorization]
        public void Update(UpdateStagingContract staging)
        {
            StagingRepository.UpdateStagingState(staging.Id, staging.State, staging.CurrentStep);
        }

        [Route("delete")]
        [HttpPost, Authorization]
        public void Delete(DeleteStagingContract staging)
        {
            StagingRepository.DeleteStaging(staging.Id);
        }

        [Route("list")]
        [HttpPost, Authorization]
        public IEnumerable<StagingInList> GetList()
        {
            return StagingRepository.GetList();
        }

        [Route("list/{id}")]
        [HttpPost, Authorization]
        public StagingInList GetInList(Guid id)
        {
            return StagingRepository.GetStagingInList(id);
        }

        [Route("{id}")]
        [HttpPost, Authorization]
        public StagingInLayer GetInLayer(Guid id)
        {
            return StagingRepository.GetStaging(id);
        }

        [Route("addserver")]
        [HttpPost, Authorization]
        public void AddServer(AddServerContract server)
        {
            StagingRepository.AddServer(server.StagingId, server);
        }
    }
}