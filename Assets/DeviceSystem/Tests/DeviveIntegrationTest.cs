using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

public class DeviveIntegrationTest : ZenjectIntegrationTestFixture
{
    private readonly GameObject devicePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/DeviceSystem/Prefabs/Device.prefab");
    private readonly float speed = 0.5f;

    [UnityTest]
    public IEnumerator AnalogDeviceTest()
    {
        Initialization();

        var deviceFactory = Container.Resolve<Device.Factory>();
        var analogAction = Container.Resolve<AnalogAction>();
        var cancelColision = Container.Resolve<CancelColisionHandler>();

        var device = deviceFactory.Create(analogAction, cancelColision);

        DeviceState deviceState = new DeviceState
        {
            position = new Vector3(1, 0, 0)
        };

        yield return null;

        device.SendAction(deviceState);

        yield return null;

        Assert.That(device.GetState().position != deviceState.position);

        yield break;
    }

    [UnityTest]
    public IEnumerator DigitalDeviceTest()
    {
        Initialization();


        var deviceFactory = Container.Resolve<Device.Factory>();
        var discreteAction = Container.Resolve<DiscreteAction>();
        var cancelColision = Container.Resolve<CancelColisionHandler>();

        var device = deviceFactory.Create(discreteAction, cancelColision);

        DeviceState deviceState = new DeviceState
        {
            position = new Vector3(1, 0, 0)
        };

        yield return null;

        device.SendAction(deviceState);

        yield return null;

        Assert.That(device.GetState().position == deviceState.position);

        yield break;
    }


    [UnityTest]
    public IEnumerator AnalogDeviceTestWithCancelColisionHandler()
    {
        Initialization();

        var deviceFactory = Container.Resolve<Device.Factory>();
        var analogAction = Container.Resolve<AnalogAction>();
        var cancelColision = Container.Resolve<CancelColisionHandler>();

        var device = deviceFactory.Create(analogAction, cancelColision);

        DeviceState targetDeviceState1 = new DeviceState
        {
            position = new Vector3(2, 0, 0)
        };

        DeviceState targetDeviceState2 = new DeviceState
        {
            position = new Vector3(-2, -2, -2)
        };

        Vector3 speedDirectionTarget1;
        Vector3 speedDirectionTarget2;

        yield return null;

        device.SendAction(targetDeviceState1);

        yield return null;
        DeviceState deviceState1 = device.GetState();

        yield return null;
        DeviceState deviceState2 = device.GetState();

        speedDirectionTarget1 = Vector3.Normalize(deviceState2.position - deviceState1.position);

        yield return null;

        device.SendAction(targetDeviceState2);

        yield return null;
        DeviceState deviceState3 = device.GetState();

        yield return null;
        DeviceState deviceState4 = device.GetState();
        speedDirectionTarget2 = Vector3.Normalize(deviceState4.position - deviceState3.position);


        Assert.NotZero((int)(speedDirectionTarget2 - speedDirectionTarget1).sqrMagnitude * 1000);

        yield break;
    }


    [UnityTest]
    public IEnumerator AnalogDeviceTestWithWaitColisionHandler()
    {
        Initialization();

        var deviceFactory = Container.Resolve<Device.Factory>();
        var analogAction = Container.Resolve<AnalogAction>();
        var waitColision = Container.Resolve<WaitingColisionHandler>();

        var device = deviceFactory.Create(analogAction, waitColision);

        yield return null;
        DeviceState targetDeviceState1 = new DeviceState();
        targetDeviceState1.position = new Vector3(2, 0, 0);
        device.SendAction(targetDeviceState1);

        yield return null;
        DeviceState deviceState1 = device.GetState();

        yield return null;
        DeviceState deviceState2 = device.GetState();

        var speedDirectionTarget1 = deviceState2.position - deviceState1.position;

        yield return null;
        DeviceState targetDeviceState2 = new DeviceState();
        targetDeviceState2.position = new Vector3(-2, 0, 0);
        device.SendAction(targetDeviceState2);

        yield return null;
        DeviceState deviceState3 = device.GetState();

        yield return null;
        DeviceState deviceState4 = device.GetState();
        var speedDirectionTarget2 = deviceState4.position - deviceState3.position;

        Assert.Zero((int)(speedDirectionTarget2 - speedDirectionTarget1).sqrMagnitude * 1000);

        while (device.GetState().position != targetDeviceState1.position)
        {
            yield return null;
        }

        yield return null;

        Assert.That(device.GetState().position != targetDeviceState1.position);

        yield break;
    }

    [UnityTest]
    public IEnumerator AnalogDeviceTestWithWarningColisionHandler()
    {
        Initialization();

        var deviceFactory = Container.Resolve<Device.Factory>();
        var analogAction = Container.Resolve<AnalogAction>();
        var warningColision = Container.Resolve<WarningColisionHandler>();

        var device = deviceFactory.Create(analogAction, warningColision);

        yield return null;
        DeviceState targetDeviceState1 = new DeviceState();
        targetDeviceState1.position = new Vector3(2, 0, 0);
        device.SendAction(targetDeviceState1);

        yield return null;
        DeviceState deviceState1 = device.GetState();

        yield return null;
        DeviceState deviceState2 = device.GetState();

        var speedDirectionTarget1 = deviceState2.position - deviceState1.position;

        yield return null;
        DeviceState targetDeviceState2 = new DeviceState();
        targetDeviceState2.position = new Vector3(-2, 0, 0);
        device.SendAction(targetDeviceState2);

        yield return null;
        DeviceState deviceState3 = device.GetState();

        yield return null;
        DeviceState deviceState4 = device.GetState();
        var speedDirectionTarget2 = deviceState4.position - deviceState3.position;

        Assert.Zero((int)(speedDirectionTarget2 - speedDirectionTarget1).sqrMagnitude * 1000);

        yield break;
    }

    private void Initialization()
    {
        PreInstall();

        Container.Bind<GameInsaller.Settings>().AsSingle().WithArguments(devicePrefab, speed);

        Container.BindFactory<IAction, IActionCollision, Device, Device.Factory>()
            .FromComponentInNewPrefab(devicePrefab)
            .WithGameObjectName("Device")
            .UnderTransformGroup("Devices");

        Container.Bind<AnalogAction>().AsSingle().WithArguments(speed);
        Container.Bind<DiscreteAction>().AsSingle();

        Container.Bind<CancelColisionHandler>().AsSingle();
        Container.Bind<WarningColisionHandler>().AsSingle();
        Container.Bind<WaitingColisionHandler>().AsSingle();

        PostInstall();
    }
}