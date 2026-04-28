using Game.Characters.Player.Scripts;
using Zenject;

public sealed class PlayerRuntimeSyncService : ITickable
{
    private readonly PlayerRuntime _playerRuntime;
    private readonly PlayerView _playerView;

    public void Tick()
    {
        _playerRuntime.SetPosition(_playerView.transform.position);
    }
}