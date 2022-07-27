using UnityEngine;
using Zenject;

public class Device : MonoBehaviour
{
    ICollisionHandler _collisionHandler;
    IChangeState _changeState;
    DeviceState _deviceState;

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

    public class Factory : PlaceholderFactory<IChangeState, ICollisionHandler, Device>
    {

    }
}