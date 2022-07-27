using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInsaller : MonoInstaller {

    [Inject]
    Settings _settings = null;

    public override void InstallBindings()
    {
        InstallDevices();
        InstallGui();
    }

    private void InstallDevices()
    {

        Container.Bind<DeviceFactory>().AsSingle();
        Container.Bind<AnalogChangeState>().WhenInjectedInto<DeviceFactory>();
        Container.Bind<CancelColisionHandler>().WhenInjectedInto<DeviceFactory>();

        Container.BindFactory<IChangeState, ICollisionHandler, Device, Device.Factory>()
            .FromComponentInNewPrefab(_settings.DevicePrefab)
            .WithGameObjectName("Device")
            .UnderTransformGroup("Devices");

        Container.BindInterfacesAndSelfTo<DeviceManager>().AsSingle();
    }
    private void InstallGui()
    {
        Container.Bind<GuiHandler>().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public GameObject DevicePrefab;
    }
}