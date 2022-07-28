using Zenject;

public class DeviceFactory
{
    private readonly Device.Factory _factory;
    private readonly AnalogAction _analogChangeState;
    private readonly DiscreteAction _digitalChangeState;
    private readonly CancelColisionHandler _cancelColisionHandler;
    private readonly WarningColisionHandler _warningColisionHandler;
    private readonly WatingColisionHandler _waitingColisionHandler;

    [Inject]
    public DeviceFactory(Device.Factory factory, AnalogAction analogChangeState, DiscreteAction digitalChangeState,
        CancelColisionHandler cancelColisionHandler, WarningColisionHandler warningColisionHandler, WatingColisionHandler waitingColisionHandler)
    {
        _factory = factory;
        _analogChangeState = analogChangeState;
        _digitalChangeState = digitalChangeState;
        _cancelColisionHandler = cancelColisionHandler;
        _warningColisionHandler = warningColisionHandler;
        _waitingColisionHandler = waitingColisionHandler;
    }

    public Device Create(Device.DeviceTypes deviceType, Device.ActionCollisionTypes collisionType)
    {
        if (deviceType == Device.DeviceTypes.Analog)
        {
            return CreateDevice(_analogChangeState, collisionType);
        }
        else if (deviceType == Device.DeviceTypes.Digital)
        {
            return CreateDevice(_digitalChangeState, collisionType);
        }

        throw new System.Exception("Incorrect device type");
    }

    private Device CreateDevice(IAction changeState, Device.ActionCollisionTypes collisionType)
    {
        if (collisionType == Device.ActionCollisionTypes.CancelAction)
        {
            return _factory.Create(changeState, _cancelColisionHandler);
        }
        else if (collisionType == Device.ActionCollisionTypes.WaitAction)
        {
            return _factory.Create(changeState, _waitingColisionHandler);
        }
        else if (collisionType == Device.ActionCollisionTypes.WarningAction)
        {
            return _factory.Create(changeState, _warningColisionHandler);
        }

        throw new System.Exception("Incorrect device type");
    }
}