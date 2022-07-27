using UnityEngine;
using Zenject;

public class GuiHandler: MonoBehaviour
{
    DeviceManager _deviceManager;

    [Inject]
    public void Construct(DeviceManager deviceManager)
    {
        _deviceManager = deviceManager;
    }

    public void AddDevice()
    {
        _deviceManager.AddDevice(Device.DeviceTypes.Analog);
    }
}