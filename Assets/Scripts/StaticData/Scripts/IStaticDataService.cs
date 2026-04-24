using Movements.Data.User;
using Rotation._ProjectFiles.Player.Scripts.Movements.Configs;

namespace StaticData.Scripts
{
    public interface IStaticDataService
    {
        PlayerMovementConfig PlayerMovementConfig { get; }
        PlayerRotationConfig PlayerRotationConfig { get; }
    }
}