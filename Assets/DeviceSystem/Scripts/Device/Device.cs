using UnityEngine;
using Zenject;

public class Device : MonoBehaviour
{
    IActionCollision _collisionHandler;
    IAction _changeState;

    DeviceState _deviceState;
    DeviceState _targerDeviceState;

    bool isBusy;

    public enum DeviceTypes { Analog, Digital, CancelAction, WaitAction, warningAction};
    private DeviceTypes _deviceType;
    public DeviceTypes DeviceType { 
        get => _deviceType; 
        set => _deviceType = value; 
    }

    public enum ActionCollisionTypes {CancelAction, WaitAction, WarningAction };
    private ActionCollisionTypes _actionCollisionType;
    public ActionCollisionTypes ActionCollisionType
    {
        get => _actionCollisionType;
        set => _actionCollisionType = value;
    }

    [Inject]
    public void Construct(IAction changeState, IActionCollision collisionHandler)
    {
        _collisionHandler = collisionHandler;
        _changeState = changeState;
    }

    public void SendAction(DeviceState newState)
    {
        if (isBusy)
        {
            _targerDeviceState = _collisionHandler.UpdateStateOnActionCollision(_deviceState, _targerDeviceState, newState);
        }
        else
        {
            _targerDeviceState = newState;
        }
    }

    private void Update()
    {
        ChageState();
        ApplayState();
        CheckActionStatus();
    }

    private void CheckActionStatus()
    {
        if ((_deviceState.position - _targerDeviceState.position).magnitude == 0)
        {
            //проверка на незавиршилось ли действие только что 
            if (isBusy)
            {
                _targerDeviceState = _collisionHandler.UpdateStateOnActionFinish(_deviceState);
            }
            else
            {
                isBusy = false;
            }
        }
        else
        {
            isBusy = true;
        }
    }

    private void ChageState()
    {
        _deviceState = _changeState.ChangeState(_deviceState, _targerDeviceState);
    }
    private void ApplayState()
    {
        transform.position = _deviceState.position;
    }

    public class Factory : PlaceholderFactory<IAction, IActionCollision, Device>
    {

    }
}