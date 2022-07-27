using UnityEngine;

public  class AnalogChangeState : IChangeState
{
    float speed = 0.1f;

    public DeviceState ChangeState(DeviceState oldState, DeviceState newState)
    {
        var deviceState = new DeviceState();
        deviceState.position = UpdatePosition(oldState.position, newState.position);
        return deviceState;
    }
    Vector3 UpdatePosition(Vector3 oldPos, Vector3 newPos)
    {
        Vector3 position = new Vector3();
        var v = newPos - oldPos;
        var delta = Time.deltaTime * speed;
        if (v.sqrMagnitude > delta)
        {
            position = oldPos + v.normalized * delta;
        }
        else
        {
            position = newPos;
        }

        return position;
    }
}