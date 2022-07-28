public interface IActionCollision
{
    DeviceState UpdateStateOnActionCollision(DeviceState currentState, DeviceState targetDeviceState, DeviceState newDeviceState);

    DeviceState UpdateStateOnActionFinish(DeviceState currentState);

}