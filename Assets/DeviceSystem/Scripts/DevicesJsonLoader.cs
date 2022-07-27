using UnityEngine;
using Zenject;

class DevicesJsonLoader : IInitializable
{
    [Inject]
    DeviceManager _deviceManager;

    string file = "Test";

    public void Initialize()
    {
        string s = ReadJsonFromFile(file);
        CreateDevicesFromJSON(s);
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

