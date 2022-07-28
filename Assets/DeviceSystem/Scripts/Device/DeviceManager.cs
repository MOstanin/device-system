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

    public void AddDevice(Device.DeviceTypes deviceType, Device.ActionCollisionTypes collisionType)
    {
        var device = _deviceFactory.Create(deviceType, collisionType);
        device.DeviceType = deviceType;
        device.ActionCollisionType = collisionType;
        _devices.Add(device);
    }

    public void SendAction(int id, Vector3 vector3)
    {
        var newState = new DeviceState(vector3);
        _devices[id]?.SendAction(newState);
    }

    public List<Device> GetDevices()
    {
        return _devices;
    }

    public string GetDeviceStringList()
    {
        string devicesTextList = "";
        for (int i = 0; i < _devices.Count; i++)
        {
            devicesTextList += "Device id: " + i + ", ";
            devicesTextList += "type: " + _devices[i].DeviceType.ToString() + ", ";
            devicesTextList += "action collison: " + _devices[i].ActionCollisionType.ToString();
            devicesTextList += "\n";
        }
        return devicesTextList;
    }
}

