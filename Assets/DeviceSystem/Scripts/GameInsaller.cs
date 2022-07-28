using System;
using UnityEngine;
using Zenject;

public class GameInsaller : MonoInstaller {

    [Inject]
    Settings _settings = null;

    public override void InstallBindings()
    {
        InstallDevices();
        InstallGui();
        InstallDevicesFormFile();
    }

    private void InstallDevices()
    {

        Container.Bind<DeviceFactory>().AsSingle();
        Container.Bind<AnalogAction>().WhenInjectedInto<DeviceFactory>();
        Container.Bind<DiscreteAction>().WhenInjectedInto<DeviceFactory>();

        Container.Bind<CancelColisionHandler>().WhenInjectedInto<DeviceFactory>();
        Container.Bind<WarningColisionHandler>().WhenInjectedInto<DeviceFactory>();
        Container.Bind<WatingColisionHandler>().WhenInjectedInto<DeviceFactory>();

        Container.BindFactory<IAction, IActionCollision, Device, Device.Factory>()
            .FromComponentInNewPrefab(_settings.DevicePrefab)
            .WithGameObjectName("Device")
            .UnderTransformGroup("Devices");

        Container.BindInterfacesAndSelfTo<DeviceManager>().AsSingle();
    }
    private void InstallGui()
    {
        Container.Bind<GuiHandler>().AsSingle();
    }
    private void InstallDevicesFormFile()
    {
        Container.BindInterfacesAndSelfTo<DevicesJsonLoader>().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public GameObject DevicePrefab;
    }
}
