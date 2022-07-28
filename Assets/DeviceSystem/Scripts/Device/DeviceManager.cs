using System;
using System.Collections.Generic;
using UnityEngine;
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

    public void AddDevice(Device.DeviceTypes deviceType)
    {
        var device = _deviceFactory.Create(deviceType);
        _devices.Add(device);
    }

    public void SendAction(int id, Vector3 vector3)
    {
        var newState = new DeviceState(vector3);
        _devices[id]?.SetState(newState);
    }

    public string GetDeviceStringList()
    {
        string devices = "";
        for (int i = 0; i < _devices.Count; i++)
        {
            devices += "Device id: " + i + "\n";
        }
        return devices;
    }

    public List<Device> GetDevices()
    {
        return _devices;
    }
}

