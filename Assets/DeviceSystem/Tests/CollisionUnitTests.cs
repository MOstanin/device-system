using Zenject;
using NUnit.Framework;

[TestFixture]
public class CollisionUnitTests : ZenjectUnitTestFixture
{
    [Test]
    public void CancelColisionHandlerTest()
    {
        Container.Bind<CancelColisionHandler>().AsSingle();
        var cancelColision = Container.Resolve<CancelColisionHandler>();

        DeviceState deviceState1, deviceState2, deviceState3, deviceStateAfterCollision, deviceStateAfterActionFinish;

        GameLogic(cancelColision, out deviceState1, out deviceState2, out deviceState3, out deviceStateAfterCollision, out deviceStateAfterActionFinish);

        Assert.AreEqual(deviceStateAfterCollision, deviceState3);

        Assert.AreEqual(deviceStateAfterActionFinish, deviceState1);
    }

    [Test]
    public void WarningCollisionHandlerTest()
    {
        Container.Bind<WarningColisionHandler>().AsSingle();
        var warningColision = Container.Resolve<WarningColisionHandler>();

        DeviceState deviceState1, deviceState2, deviceState3, deviceStateAfterCollision, deviceStateAfterActionFinish;

        GameLogic(warningColision, out deviceState1, out deviceState2, out deviceState3, out deviceStateAfterCollision, out deviceStateAfterActionFinish);

        Assert.AreEqual(deviceStateAfterCollision, deviceState2);

        Assert.AreEqual(deviceStateAfterActionFinish, deviceState1);
    }

    [Test]
    public void WaitingCollisionHandlerTest()
    {
        Container.Bind<WaitingColisionHandler>().AsSingle();

        var waitingColision = Container.Resolve<WaitingColisionHandler>();

        DeviceState deviceState1, deviceState2, deviceState3, deviceStateAfterCollision, deviceStateAfterActionFinish;
        GameLogic(waitingColision, out deviceState1, out deviceState2, out deviceState3, out deviceStateAfterCollision, out deviceStateAfterActionFinish);

        Assert.AreEqual(deviceStateAfterCollision, deviceState2);

        Assert.AreEqual(deviceStateAfterActionFinish, deviceState3);
    }

    private static void GameLogic(IActionCollision colisionHandler, out DeviceState deviceState1, out DeviceState deviceState2, out DeviceState deviceState3,
    out DeviceState deviceStateAfterCollision, out DeviceState deviceStateAfterActionFinish)
    {
        deviceState1 = new DeviceState
        {
            position = new UnityEngine.Vector3(1, 1, 1)
        };
        deviceState2 = new DeviceState
        {
            position = new UnityEngine.Vector3(2, 2, 2)
        };
        deviceState3 = new DeviceState
        {
            position = new UnityEngine.Vector3(1, 2, 3)
        };

        deviceStateAfterCollision = colisionHandler.UpdateStateOnActionCollision(deviceState1, deviceState2, deviceState3);
        deviceStateAfterActionFinish = colisionHandler.UpdateStateOnActionFinish(deviceState1);
    }
}