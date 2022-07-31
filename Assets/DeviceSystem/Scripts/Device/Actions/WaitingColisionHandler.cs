public class WaitingColisionHandler : IActionCollision
{
    private DeviceState nextDeviceState;
    public DeviceState UpdateStateOnActionCollision(DeviceState currentState, DeviceState targetDeviceState, DeviceState newDeviceState)
    {
        SaveAction(newDeviceState);
        return targetDeviceState;
    }

    private void SaveAction(DeviceState newDeviceState)
    {
        nextDeviceState = newDeviceState;
    }

    public DeviceState UpdateStateOnActionFinish(DeviceState currentState)
    {
        return nextDeviceState;
    }
}