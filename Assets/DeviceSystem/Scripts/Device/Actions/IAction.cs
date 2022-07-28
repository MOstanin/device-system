public interface IAction
{
    public DeviceState ChangeState(DeviceState oldState, DeviceState newState);
}