using Game.Characters.Player.Movements.Data;
using Game.Characters.Player.Rotation;
using Game.Infrastructure.ResourceLoaders;

namespace Game.Infrastructure.StaticData
{
    public sealed class StaticDataService : IStaticDataService
    {
        private const string PlayerMovementConfigPath = "Configs/PlayerMovementConfig";
        private const string PlayerRotationConfigPath = "Configs/PlayerRotationConfig";

        private readonly IResourceLoader _resourceLoader;

        public StaticDataService(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;

            LoadMovementConfig();
            LoadRotationConfig();
        }

        public PlayerMovementConfig PlayerMovementConfig { get; private set; }
        public PlayerRotationConfig PlayerRotationConfig { get; private set; }

        private void LoadMovementConfig()
        {
            PlayerMovementConfig = _resourceLoader.Load<PlayerMovementConfig>(PlayerMovementConfigPath);
        }

        private void LoadRotationConfig()
        {
            PlayerRotationConfig = _resourceLoader.Load<PlayerRotationConfig>(PlayerRotationConfigPath);
        }
    }
}
