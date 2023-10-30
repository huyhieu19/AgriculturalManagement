﻿using Microsoft.AspNetCore.Mvc;
using Models;
using MQTTProcess;
using Service;
using Service.Contracts;
using Service.Contracts.ESP;

namespace AgriculturalManagement.Controllers.ESP
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IDataStatisticsService dataStatisticsService;
        private readonly IEspBackgroundProcessService espBackgroundProcessService;
        private readonly IRestartAsyncMQTTService customServiceStopper;
        public EspController(IServiceManager serviceManager
            , IDataStatisticsService dataStatisticsService
            , IEspBackgroundProcessService espBackgroundProcessService,
            IRestartAsyncMQTTService customServiceStopper)
        {
            _serviceManager = serviceManager;
            this.dataStatisticsService = dataStatisticsService;
            this.espBackgroundProcessService = espBackgroundProcessService;
            this.customServiceStopper = customServiceStopper;
        }

        [HttpGet("esps")]
        public async Task<List<EspDisplayModel>> GetAll() => await _serviceManager.EspService.GetAll();

        [HttpPost("esp")]
        public async Task<ActionResult<bool>> Create(EspCreateModel model)
        {
            return await _serviceManager.EspService.CreateEsp(model);
        }

        //[HttpGet("Restart")]
        //public async Task<bool> Restart()
        //{
        //    return await customServiceStopper.RestartJobBackground();
        //}
    }
}
