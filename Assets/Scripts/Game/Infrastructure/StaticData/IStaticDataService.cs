using Game.Characters.Player.Movements.Data.User;
using Game.Characters.Player.Rotation._ProjectFiles.Player.Scripts.Movements.Configs;

namespace Game.Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        PlayerMovementConfig PlayerMovementConfig { get; }
        PlayerRotationConfig PlayerRotationConfig { get; }
    }
}