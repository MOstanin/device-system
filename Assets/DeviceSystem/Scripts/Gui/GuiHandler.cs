using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GuiHandler : MonoBehaviour
{
    public InputField idField;
    public InputField xField;
    public InputField yField;
    public InputField zField;

    public Text deviceList;
    private DeviceManager _deviceManager;
    private DevicesJsonLoader _devicesJsonLoader;

    [Inject]
    public void Construct(DeviceManager deviceManager, DevicesJsonLoader devicesJsonLoader)
    {
        _deviceManager = deviceManager;
        _devicesJsonLoader = devicesJsonLoader;
    }

    public void AddAnalogDevice()
    {
        _deviceManager.AddDevice(Device.DeviceTypes.Analog, Device.ActionCollisionTypes.WaitAction);
    }

    public void AddDigitalDevice()
    {
        _deviceManager.AddDevice(Device.DeviceTypes.Digital, Device.ActionCollisionTypes.WarningAction);
    }

    public void SendAction()
    {
        var vector3 = new Vector3(float.Parse(xField.text), float.Parse(yField.text), float.Parse(zField.text));
        var id = int.Parse(idField.text);
        _deviceManager.SendAction(id, vector3);
    }

    public void FillDeviceList()
    {
        deviceList.text = _deviceManager.GetDeviceStringList();
    }

    public void SaveDevices()
    {
        _devicesJsonLoader.PrintDevicesAsJson();
    }

    private void Update()
    {
        FillDeviceList();
    }
}