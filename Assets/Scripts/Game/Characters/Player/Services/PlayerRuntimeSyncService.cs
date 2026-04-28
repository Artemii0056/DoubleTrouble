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
        private readonly PlayerRuntime _runtime;
        private readonly PlayerView _view;
        private readonly TargetRuntimeRegistry _targetRegistry;
        private DamageableRuntimeRegistry _damageableRegistry;

        public PlayerRuntimeSyncService(
            PlayerRuntime runtime,
            PlayerView view,
            TargetRuntimeRegistry targetRegistry, 
            DamageableRuntimeRegistry damageableRegistry)
        {
            _runtime = runtime;
            _view = view;
            _targetRegistry = targetRegistry;
            _damageableRegistry = damageableRegistry;
        }

        public void Initialize()
        {
            _runtime.SetPosition(_view.Transform.position);
            _targetRegistry.Register(_runtime);
            
            _targetRegistry.Register(_runtime);
            _damageableRegistry.Register(_runtime);
        }

        public void Tick()
        {
            _runtime.SetPosition(_view.Transform.position);
        }

        public void Dispose()
        {
            _targetRegistry.Unregister(_runtime.Id);
        }
    }
}