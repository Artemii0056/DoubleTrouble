using System;
using Game.Characters.Enemy.Runtime;
using Game.Characters.Enemy.Services;
using Game.Characters.Player.Runtume;
using Game.Characters.Player.Scripts;
using Zenject;

namespace Game.Characters.Player.Services
{
    public sealed class PlayerRuntimeSyncService : IInitializable, ITickable, IDisposable
    {
        private readonly Runtume.Player _runtime;
        private readonly PlayerView _view;
        private readonly TargetRuntimeStore _targetStore;
        private readonly DamageableRuntimeStore _damageableStore;

        public PlayerRuntimeSyncService(
            Runtume.Player runtime,
            PlayerView view,
            TargetRuntimeStore targetStore, 
            DamageableRuntimeStore damageableStore)
        {
            _runtime = runtime;
            _view = view;
            _targetStore = targetStore;
            _damageableStore = damageableStore;
        }

        public void Initialize()
        {
            _runtime.SetPosition(_view.Transform.position);
            _targetStore.Register(_runtime);
            
            _damageableStore.Register(_runtime);
        }

        public void Tick()
        {
            _runtime.SetPosition(_view.Transform.position);
        }

        public void Dispose()
        {
            _targetStore.Unregister(_runtime.Id);
            _damageableStore.Unregister(_runtime.Id);
        }
    }
}