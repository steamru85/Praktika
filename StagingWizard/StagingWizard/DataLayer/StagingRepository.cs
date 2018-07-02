﻿using Npgsql;
using StagingWizard.DataLayerContracts;
using StagingWizard.UIContracts;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Threading.Tasks;

namespace StagingWizard.DataLayer
{
    public class StagingRepository : IStagingRepository
    {
        public StagingRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }

        public IDbConnection OpenConnection(string connStr)
        {
            var conn = new NpgsqlConnection(connStr);
            conn.Open();
            return conn;
        }

        public void AddServer(Guid stagingId, Server server)
        {
            using (var conn = OpenConnection(ConnectionString))
            {
                conn.Execute("INSERT INTO servers(id, stagingid, ip, serverrole, created, services) VALUES(@id, @stagingid, @ip, @serverrole, @created, @services)", new { id = server.Id, stagingid = stagingId, ip = server.IP, serverrole = server.ServerRole, created = server.Created, services = server.Services });
            }
        }

        public void CreateStaging(Guid id, string creator, EStagingState state, string currentStep, string inputParams)
        {
            var created = DateTime.UtcNow;
            using (var conn = OpenConnection(ConnectionString))
            {
                conn.Execute("INSERT INTO stagings(id, creator, created, lastupdated, state, currentstep, inputparams) VALUES(@id, @creator, @created, @lastupdated, @state, @currentstep, @inputparams)", new { id, creator, state = state.ToString(), currentStep, created, lastupdated = created, inputparams = inputParams });
            }
        }

        public void DeleteStaging(Guid id)
        {
            using (var conn = OpenConnection(ConnectionString))
            {
                conn.Execute("delete from stagings where id = @id; delete from servers where stagingid = @id;", new { id });
            }
        }

        public IEnumerable<StagingInList> GetList()
        {
            using (var conn = OpenConnection(ConnectionString))
            {
                return conn.Query<StagingInList>("select * from stagings");
            }
        }

        public StagingInLayer GetStaging(Guid id)
        {
            using (var conn = OpenConnection(ConnectionString))
            {
                var staging = conn.QueryFirst<StagingInLayer>("select * from stagings where id = @id", new { id });
                staging.Servers = conn.Query<Server>("select * from servers where stagingid = @id", new { id });
                return staging;
            }            
        }

        public void UpdateStagingState(Guid id, EStagingState state, string currentstep)
        {
            using (var conn = OpenConnection(ConnectionString))
            {
                conn.Execute("update stagings set state = @state, currentstep = @currentstep where id = @id", new { id, state = state.ToString(), currentstep });
            }
        }

        public StagingInList GetStagingInList(Guid id)
        {
            using (var conn = OpenConnection(ConnectionString))
            {
                return conn.QueryFirst<StagingInList>("select * from stagings where id = @id", new { id });
            }
        }
    }
}
