using AutoMapper;
using Common.Enum;
using Entities;
using Models;

namespace Common.ResolveProfileMapper;

public class DeviceDriverCountResolver : IValueResolver<ZoneEntity, ZoneDisplayModel, int?>
{
    public int? Resolve(ZoneEntity source, ZoneDisplayModel destination, int? member, ResolutionContext context)
    {
        // Tính toán số lượng Device có DeviceType = 1
        return source.Devices?.Count(device => device.DeviceType == DeviceType.W);
    }
}

public class InstrumentationCountResolver : IValueResolver<ZoneEntity, ZoneDisplayModel, int?>
{
    public int? Resolve(ZoneEntity source, ZoneDisplayModel destination, int? member, ResolutionContext context)
    {
        // Tính toán số lượng Device có DeviceType = 2
        return source.Devices?.Count(device => device.DeviceType == DeviceType.R);
    }
}