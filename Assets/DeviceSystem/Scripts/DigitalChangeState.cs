public class DigitalChangeState : IChangeState
{

    public DeviceState ChangeState(DeviceState oldState, DeviceState newState)
    {
        return newState;
    }
}