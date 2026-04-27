using Input.InputReader.Scripts;
using Movements;
using Player.Scripts;
using ResourceLoaders.Scripts;
using Rotation;
using ShootSystem.Scripts;
using StaticData.Scripts;
using TestSystem;
using TestSystem.TestProjectileLogic;
using TestSystem.TestProjectileLogic.Statuses;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private TargetScanner _targetScanner;
        
        public override void InstallBindings()
        {
            Container.Bind<WeaponShooter>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            CharacterBindings();
            InfrastructureBindings();
            
            Container.Bind<IPlayerTransform>()
                .FromInstance(_playerView)
                .AsSingle();
            
            Container.Bind<TargetScanner>()
                .FromInstance(_targetScanner)
                .AsSingle();
            
            Container.Bind<CombatServices>().AsTransient(); // Убрать костылек
            
            Container.Bind<ProjectileCollisionService>().AsSingle();
            Container.Bind<TargetSearchService>().AsSingle();
            Container.Bind<StatusEffectService>().AsSingle();
            Container.Bind<DamageService>().AsSingle();
            Container.Bind<ProjectileHitService>().AsSingle();
            
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

        public void CharacterBindings()
        {
            Container.Bind<IPlayerInputReader>()
                .To<PlayerInputReader>()
                .AsSingle();
            
            Container.BindInterfacesTo<PlayerMover>().AsSingle(); 
            Container.BindInterfacesTo<PlayerRotationController>().AsSingle(); 
            
            Container.BindInterfacesAndSelfTo<TargetRotationProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputRotationTargetProvider>().AsSingle();
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