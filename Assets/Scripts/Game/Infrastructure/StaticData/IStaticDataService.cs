using Game.Characters.Player.Movements.Data;
using Game.Characters.Player.Rotation;

namespace Game.Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        PlayerMovementConfig PlayerMovementConfig { get; }
        PlayerRotationConfig PlayerRotationConfig { get; }
    }
}
