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
    public DeviceFactory(Device.Factory factory, AnalogChangeState analogChangeState, CancelColisionHandler cancelColisionHandler)
    {
        _factory = factory;
        _analogChangeState = analogChangeState;
        _cancelColisionHandler = cancelColisionHandler;
    }

    public Device Create(Device.DeviceTypes type)
    {
        if (type == Device.DeviceTypes.Analog)
        {
            return _factory.Create(_analogChangeState, _cancelColisionHandler);

        }
        else if (type == Device.DeviceTypes.Digital)
        {
            return _factory.Create(_analogChangeState, _cancelColisionHandler);
        }

        throw new System.Exception("Incorrect type");
    }
}