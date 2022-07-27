using UnityEngine;

public interface IChangeState
{
    public DeviceState ChangeState(DeviceState oldState, DeviceState newState);
}