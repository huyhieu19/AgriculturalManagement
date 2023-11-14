namespace Models.DeviceAuto
{
    public class DeviceDriverTurnOnTurnOffModel
    {
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? IsDaily { get; set; } = false;
        public string? Note { get; set; }
        public DateTime? ShutDownTimer { get; set; }
        public DateTime? OpenTimer { get; set; }
        // Case: Device return value number
        public double? ThresholdValueOn { get; set; }
        public double? ThresholdValue { get; set; }
        public double? ThresholdValueOff { get; set; }
        // Case: Device return value true or false
        public bool? IsAffected { get; set; }
        public bool IsSuccess { get; set; } = false;
        public bool IsRemove { get; set; } = false;
        public int DeviceDriverId { get; set; }

        #region In device driver
        public bool IsAction { get; set; }
        public bool IsAuto { get; set; }
        #endregion
    }
}