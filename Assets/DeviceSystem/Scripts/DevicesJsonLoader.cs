﻿using UnityEngine;
using Zenject;

public class DevicesJsonLoader : IInitializable
{
    [Inject]
    DeviceManager _deviceManager;

    string file = "Test";

    public void Initialize()
    {
        string s = ReadJsonFromFile(file);
        CreateDevicesFromJSON(s);
    }

    public void PrintDevicesAsJson()
    {
        var deviceList = _deviceManager.GetDevices();
        var scheme = new DevicesScheme();
        scheme.deviceArray = new DevicesScheme.Device[deviceList.Count];

        for (int i = 0; i < deviceList.Count; i++)
        {
            scheme.deviceArray[i] = new DevicesScheme.Device();
            if (deviceList[i].DeviceType == Device.DeviceTypes.Analog)
            {
                scheme.deviceArray[i].deviceType = 1;
            }
            else
            {
                scheme.deviceArray[i].deviceType = 2;
            }
        }
        var s = JsonUtility.ToJson(scheme);
        Debug.Log(s);
    }

    private string ReadJsonFromFile(string file)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(file);
        if (textAsset != null)
        {
            return textAsset.text;
        }
        throw new System.Exception("Incorrect JSON file name.");
    }

    private void CreateDevicesFromJSON(string jsonString)
    {
        DevicesScheme deviceScheme = JsonUtility.FromJson<DevicesScheme>(jsonString);
        
        if (deviceScheme.deviceArray == null)
        {
            throw new System.Exception("Incorrect JSON scheme.");
        }

        foreach (var device in deviceScheme.deviceArray)
        {
            if (device.deviceType == 1)
            {
                _deviceManager.AddDevice(Device.DeviceTypes.Analog);
            }
            else
            {
                _deviceManager.AddDevice(Device.DeviceTypes.Digital);
            }
        }
    }
}

