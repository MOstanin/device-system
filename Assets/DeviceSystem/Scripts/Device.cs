using System;
using UnityEngine;
using Zenject;

public class Device : MonoBehaviour
{
    ICollisionHandler _collisionHandler;
    IChangeState _changeState;
    DeviceState _deviceState;
    DeviceState _targerDeviceState;


    public enum DeviceTypes { Analog, Digital };
    private DeviceTypes deviceType;

    public DeviceTypes DeviceType { 
        get => deviceType; 
        private set => deviceType = value; 
    }

    [Inject]
    public void Construct(IChangeState changeState, ICollisionHandler collisionHandler)
    {
        _collisionHandler = collisionHandler;
        _changeState = changeState;
    }

    public void SetState(DeviceState newState)
    {
        _targerDeviceState = newState;
    }

    private void Update()
    {
        ChageState();
        ApplayState();
    }

    private void ChageState()
    {
        _deviceState = _changeState.ChangeState(_deviceState, _targerDeviceState);
    }
    private void ApplayState()
    {
        transform.position = _deviceState.position;
    }

    public class Factory : PlaceholderFactory<IChangeState, ICollisionHandler, Device>
    {

    }
}