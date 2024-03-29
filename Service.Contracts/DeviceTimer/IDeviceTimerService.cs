﻿using Models.DeviceTimer;

namespace Service.Contracts.DeviceTimer
{
    public interface IDeviceTimerService
    {
        /*
         * Screen set timer
         */
        // Set Timer for device driver
        //Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimerAvailable();


        #region Timer
        Task<List<TimerDeviceDriverDisplayModel>> GetTimerAvailableOfUser(string userId);
        Task<List<TimerDeviceDriverDisplayModel>> GetTimerAvailableOfUserForUI(string userId);
        Task<bool> CreateTimer(TimerDeviceDriverCreateModel model);
        Task<bool> UpdateTimer(TimerDeviceDriverUpdateModel model);
        Task<bool> RemoveTimer(int timerId, Guid deviceId);

        Task<List<TimerDeviceDriverDisplayModel>> GetAllTimerHistoryByDeviceId(Guid deviceId);
        Task<List<TimerDeviceDriverDisplayModel>> GetAllTimerHistoryByUser(string userId);
        #endregion

    }
}