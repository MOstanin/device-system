using System;
using System.Collections.Generic;
using Zenject;

public class DeviceManager
{
    readonly List<Device> _devices = new List<Device>();
    readonly DeviceFactory _deviceFactory;

    [Inject]
    public DeviceManager(DeviceFactory deviceFactory)
    {
        _deviceFactory = deviceFactory;
    }

    public void AddDevice(Device.DeviceTypes analog)
    {
        var device = _deviceFactory.Create(analog);
        _devices.Add(device);
    }
}