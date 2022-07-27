using System;
using Zenject;

public class DeviceFactory 
{
    readonly Device.Factory _factory;

    readonly AnalogChangeState _analogChangeState;
    readonly DigitalChangeState _digitalChangeState;

    readonly CancelColisionHandler _cancelColisionHandler;
    readonly WarningColisionHandler _warningColisionHandler;
    readonly WatingColisionHandler _waitingColisionHandler;

    [Inject]
    public DeviceFactory(Device.Factory factory, AnalogChangeState analogChangeState, DigitalChangeState digitalChangeState, 
        CancelColisionHandler cancelColisionHandler, WarningColisionHandler warningColisionHandler, WatingColisionHandler waitingColisionHandler)
    {
        _factory = factory;
        _analogChangeState = analogChangeState;
        _digitalChangeState = digitalChangeState;
        _cancelColisionHandler = cancelColisionHandler;
        _warningColisionHandler = warningColisionHandler;
        _waitingColisionHandler = waitingColisionHandler;
    }

    public Device Create(Device.DeviceTypes type)
    {
        if (type == Device.DeviceTypes.Analog)
        {
            return _factory.Create(_analogChangeState, _cancelColisionHandler);

        }
        else if (type == Device.DeviceTypes.Digital)
        {
            return _factory.Create(_digitalChangeState, _cancelColisionHandler);
        }

        throw new System.Exception("Incorrect type");
    }
}