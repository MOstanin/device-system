public class DiscreteAction : IAction
{

    public DeviceState ChangeState(DeviceState oldState, DeviceState newState)
    {
        return newState;
    }
}