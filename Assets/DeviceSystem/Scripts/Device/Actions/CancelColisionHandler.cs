public class CancelColisionHandler : IActionCollision
{
    public DeviceState UpdateStateOnActionFinish(DeviceState currentState)
    {
        return currentState;
    }

    public DeviceState UpdateStateOnActionCollision(DeviceState currentState, DeviceState targetDeviceState, DeviceState newDeviceState)
    {
        return newDeviceState;
    }
}