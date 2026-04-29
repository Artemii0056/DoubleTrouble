using Game.Characters.Enemy;
using Game.Characters.Enemy.Runtime;
using Game.Characters.Enemy.Services;
using Game.Characters.Enemy.View;
using Game.Characters.Player.Movements;
using Game.Characters.Player.Rotation;
using Game.Characters.Player.Runtume;
using Game.Characters.Player.Scripts;
using Game.Characters.Player.Services;
using Game.Combat;
using Game.Combat.Damage;
using Game.Combat.Targeting;
using Game.Core.IdServices;
using Game.Infrastructure.ResourceLoaders;
using Game.Infrastructure.StaticData;
using Game.Input.InputReader.Scripts;
using Game.Projectiles.Services;
using Game.Projectiles.View;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PlayerView _playerView;
        [FormerlySerializedAs("_targetScanner")] [SerializeField] private PhysicsTargetScanner physicsTargetScanner;

        public override void InstallBindings()
        {
            Container.Bind<CombatRegistry>().AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyAttackSystem>().AsSingle();
            
            InfrastructureBindings();
            IdBindings();

            PlayerBindings();
            EnemyBindings();

            CombatBindings();
            ProjectileBindings();
        }

        private void IdBindings()
        {
            Container.Bind<IGlobalServiceId>()
                .To<GlobalServiceId>()
                .AsSingle();
        }

        private void PlayerBindings()
        {
            Container.Bind<PlayerView>()
                .FromInstance(_playerView)
                .AsSingle();

            Container.Bind<IPlayerTransform>()
                .FromInstance(_playerView)
                .AsSingle();

            Container.Bind<IPlayerInputReader>()
                .To<PlayerInputReader>()
                .AsSingle();

            Container.Bind<Player>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerRuntimeSyncService>()
                .AsSingle();

            Container.BindInterfacesTo<PlayerMover>()
                .AsSingle();

            Container.BindInterfacesTo<PlayerRotationController>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<TargetRotationProvider>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<InputRotationTargetProvider>()
                .AsSingle();
        }

        private void EnemyBindings()
        {
            Container.Bind<EnemyRuntimeStore>()
                .AsSingle();

            Container.Bind<EnemyViewStore>()
                .AsSingle();

            Container.Bind<EnemyFactory>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyChaseSystem>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyViewSyncService>()
                .AsSingle();
        }

        private void CombatBindings()
        {
            Container.Bind<PhysicsTargetScanner>()
                .FromInstance(physicsTargetScanner)
                .AsSingle();

            Container.Bind<WeaponShooter>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<TargetSelectionService>()
                .AsSingle();

            Container.Bind<DamageService>()
                .AsSingle();

            Container.Bind<ProjectileHitSystem>()
                .AsSingle();

            Container.Bind<ProjectileCollisionSystem>()
                .AsSingle();
        }

        private void ProjectileBindings()
        {
            Container.Bind<IProjectileFactory>()
                .To<ProjectileFactory>()
                .AsSingle();

            Container.Bind<IProjectileSpawnService>()
                .To<ProjectileSpawnService>()
                .AsSingle();

            Container.Bind<IProjectileSimulationService>()
                .To<ProjectileSimulationService>()
                .AsSingle();

            Container.Bind<IProjectileViewService>()
                .To<ProjectileViewService>()
                .AsSingle();
        }

        private void InfrastructureBindings()
        {
            Container.Bind<IStaticDataService>()
                .To<StaticDataService>()
                .AsSingle();

            Container.Bind<IResourceLoader>()
                .To<ResourceLoader>()
                .AsSingle();
        }
    }
}
