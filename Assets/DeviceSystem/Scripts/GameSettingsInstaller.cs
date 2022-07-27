using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "DeviceSystem/Game Settings")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public GameInsaller.Settings GameInstaller;

    public override void InstallBindings()
    {
        Container.BindInstance(GameInstaller);
    }
}
