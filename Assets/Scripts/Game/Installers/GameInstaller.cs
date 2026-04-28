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
using Game.Combat.Services;
using Game.Combat.Statuses;
using Game.Combat.Targeting;
using Game.Core.IdServices;
using Game.Infrastructure.ResourceLoaders;
using Game.Infrastructure.StaticData;
using Game.Input.InputReader.Scripts;
using Game.Projectiles.Services;
using Game.Projectiles.View;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private TargetScanner _targetScanner;

        public override void InstallBindings()
        {
            Container.Bind<DamageableRuntimeRegistry>().AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyAttackService>().AsSingle();
            
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

            Container.Bind<PlayerRuntime>()
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
            Container.Bind<TargetRuntimeRegistry>()
                .AsSingle();

            Container.Bind<EnemyRuntimeStorage>()
                .AsSingle();

            Container.Bind<EnemyViewRegistry>()
                .AsSingle();

            Container.Bind<EnemyFactory>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyChaseService>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyViewSyncService>()
                .AsSingle();
        }

        private void CombatBindings()
        {
            Container.Bind<TargetScanner>()
                .FromInstance(_targetScanner)
                .AsSingle();

            Container.Bind<WeaponShooter>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<CombatServices>()
                .AsTransient();

            Container.Bind<TargetSearchService>()
                .AsSingle();

            Container.Bind<StatusEffectService>()
                .AsSingle();

            Container.Bind<DamageService>()
                .AsSingle();

            Container.Bind<ProjectileHitService>()
                .AsSingle();

            Container.Bind<ProjectileCollisionService>()
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