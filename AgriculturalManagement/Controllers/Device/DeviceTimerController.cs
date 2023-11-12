﻿using Microsoft.AspNetCore.Mvc;
using Models.DeviceTimer;
using Service.Contracts;

namespace AgriculturalManagement.Controllers.Device
{
    [Route("api/timer")]
    [ApiController]
    public class DeviceTimerController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly IHttpContextAccessor _contextAccessor;
        public DeviceTimerController(IServiceManager _service, IHttpContextAccessor _contextAccessor)
        {
            this._service = _service;
            this._contextAccessor = _contextAccessor;
        }
        [HttpPost, Route("g/all-available")]
        public async Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimerAvailableOfUser()
        {
            var userId = _contextAccessor.HttpContext!.User.FindFirst("Id")!.Value;
            return await _service.DeviceDriver.GetTimerAvailableOfUser(userId);
        }
        //[HttpPost, Route("get-by-device")]
        //public async Task<List<TimerDeviceDriverDisplayModel>> GetAllTimerByDeviceId(int Id)
        //{
        //    return await service.DeviceDriver.GetAllTimerByDeviceId(Id);
        //}
        [HttpPost, Route("c")]
        public async Task<bool> CreateTimer(TimerDeviceDriverCreateModel model)
        {
            return await _service.DeviceDriver.CreateTimer(model);
        }
        [HttpPost, Route("u")]
        public async Task<bool> UpdateTimer(TimerDeviceDriverUpdateModel model)
        {
            return await _service.DeviceDriver.UpdateTimer(model);
        }
        [HttpPost, Route("r")]
        public async Task<bool> RemoveTimer(int Id, Guid deviceId)
        {
            return await _service.DeviceDriver.RemoveTimer(Id, deviceId);
        }
        [HttpPost, Route("g/histories-by-device")]
        public async Task<List<TimerDeviceDriverDisplayModel>> GetAllTimerHistoryByDeviceId(Guid Id)
        {
            return await _service.DeviceDriver.GetAllTimerHistoryByDeviceId(Id);
        }
        [HttpPost, Route("g/histories")]
        public async Task<List<TimerDeviceDriverDisplayModel>> GetAllTimerHistory()
        {
            return await _service.DeviceDriver.GetAllTimerHistory();
        }
    }
}