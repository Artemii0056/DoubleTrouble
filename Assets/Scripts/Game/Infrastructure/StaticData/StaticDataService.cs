using Game.Characters.Player.Movements.Data.User;
using Game.Characters.Player.Rotation._ProjectFiles.Player.Scripts.Movements.Configs;
using Game.Infrastructure.ResourceLoaders;

namespace Game.Infrastructure.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string PlayerMovementConfigPath = "Configs/PlayerMovementConfig";
        private const string PlayerRotationConfigPath = "Configs/PlayerRotationConfig";

        private readonly IResourceLoader _resourceLoader;

        public StaticDataService(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;

            LoadRotationConfig();
            LoadMovementConfig();
        }

        public PlayerMovementConfig PlayerMovementConfig { get; private set; }
        public PlayerRotationConfig PlayerRotationConfig { get; private set; }

        private void LoadRotationConfig() =>
            PlayerMovementConfig = _resourceLoader.Load<PlayerMovementConfig>(PlayerMovementConfigPath);
        
        private void LoadMovementConfig() =>
            PlayerRotationConfig = _resourceLoader.Load<PlayerRotationConfig>(PlayerRotationConfigPath);
    }
}