using UnityEngine;

public class WarningColisionHandler : IActionCollision
{
    public DeviceState UpdateStateOnActionFinish(DeviceState currentState)
    {
        return currentState;
    }

    public DeviceState UpdateStateOnActionCollision(DeviceState currentState, DeviceState targetDeviceState, DeviceState newDeviceState)
    {
        Debug.Log("Device is busy");
        return targetDeviceState;
    }
}