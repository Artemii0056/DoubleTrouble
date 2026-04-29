using System;
using Game.Characters.Player.Runtume;
using Game.Characters.Player.Scripts;
using Game.Combat.Targeting;
using Zenject;

namespace Game.Characters.Player.Services
{
    public sealed class PlayerRuntimeSyncService : IInitializable, ITickable, IDisposable
    {
        private readonly Runtume.Player _runtime;
        private readonly PlayerView _view;
        private readonly CombatRegistry _combatRegistry;

        public PlayerRuntimeSyncService(
            Runtume.Player runtime,
            PlayerView view,
            CombatRegistry combatRegistry)
        {
            _runtime = runtime;
            _view = view;
            _combatRegistry = combatRegistry;
        }

        public void Initialize()
        {
            _runtime.SetPosition(_view.Transform.position);
            _combatRegistry.RegisterTarget(_runtime);
            _combatRegistry.RegisterDamageable(_runtime);
        }

        public void Tick()
        {
            _runtime.SetPosition(_view.Transform.position);
        }

        public void Dispose()
        {
            _combatRegistry.UnregisterTarget(_runtime.Id);
            _combatRegistry.UnregisterDamageable(_runtime.Id);
        }
    }
}
