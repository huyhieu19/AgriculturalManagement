using AutoMapper;
using Common.Queries;
using Dapper;
using Database;
using Entities;
using Microsoft.Extensions.Hosting;
using Models;
using Models.DeviceDriver;
using Repository.Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Service.BackgroundJob
{
    public class JobForDeviceDriverService : BackgroundService
    {
        private readonly ILoggerManager logger;
        private readonly DapperContext dapperContext;


        public JobForDeviceDriverService(ILoggerManager logger, DapperContext dapperContext)
        {
            this.logger = logger;
            this.dapperContext = dapperContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
             // Run every 5 seconds
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInfomation("1 Background job is running...");
                await ToDoAsyncIsAuto();
                logger.LogInfomation("2 Background job is running...");
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }

        private async Task<IEnumerable<DeviceDriverTurnOnTurnOffModel>> GetDeviceDriverTurnOnTurnOffModels()
        {
            logger.LogInfomation("GetDeviceDriverTurnOnTurnOffModels - start");
            var query = DeviceDriverQuery.GetTurnOnAndTurnOff;
            IEnumerable<DeviceDriverTurnOnTurnOffModel> listTime;
            using (var connection = dapperContext.CreateConnection())
            {
                listTime = await connection.QueryAsync<DeviceDriverTurnOnTurnOffModel>(query);
            }
            logger.LogInfomation("GetDeviceDriverTurnOnTurnOffModels - end");
            return listTime;
        }

        private async Task DeviceDriverTurnOn(int DeviceDriverId)
        {
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    await connection.ExecuteAsync(DeviceDriverQuery.UpdateTurnOn, new { Id = DeviceDriverId }, transaction: trans);
                    trans.Commit();
                }
                connection.Close();
            }
        }
        private async Task DeviceDriverTurnOff(int DeviceDriverId)
        {
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    await connection.ExecuteAsync(DeviceDriverQuery.UpdateTurnOff, new { Id = DeviceDriverId }, transaction: trans);
                    trans.Commit();
                }
                connection.Close();
            }
        }

        private async Task ToDoAsyncIsAuto()
        {
            
            var listTime = await GetDeviceDriverTurnOnTurnOffModels();

            if (listTime != null && listTime.Any(p => p.OpenTimer == 1 && p.IsAuto == true))
            {
                var entities = listTime.Where(p => p.OpenTimer == 1)!.ToList();

                foreach(var entity in entities)
                {
                    await DeviceDriverTurnOn(entity!.Id);
                }
            }


            if (listTime != null && listTime.Any(p => p.ShutDownTime == 2 && p.IsAuto == true))
            {
                var entities = listTime.Where(p => p.ShutDownTime == 2)!.ToList();

                foreach (var entity in entities)
                {
                    await DeviceDriverTurnOff(entity!.Id);
                }
            }
        }
    }
}
