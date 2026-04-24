using Input.InputReader.Scripts;
using Movements;
using ResourceLoaders.Scripts;
using Rotation;
using StaticData.Scripts;
using Zenject;

namespace DI
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            CharacterBindings();
            InfrastructureBindings();
        }

        public void CharacterBindings()
        {
            Container.Bind<IPlayerInputReader>()
                .To<PlayerInputReader>()
                .AsSingle();
            
            Container.BindInterfacesTo<PlayerMover>().AsSingle(); 
            Container.BindInterfacesTo<PlayerRotator>().AsSingle(); 
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