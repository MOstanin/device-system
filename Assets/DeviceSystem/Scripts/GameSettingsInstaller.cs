using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "DeviceSystem/Game Settings")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public GameInsaller.Settings _gameInstaller;

    public override void InstallBindings()
    {
        Container.BindInstance(_gameInstaller);
    }
}
