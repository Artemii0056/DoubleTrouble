using Game.Core.IdServices;
using PlayerRuntime = Game.Characters.Player.Runtime.Player;

namespace Game.Characters.Player.Services
{
    public sealed class PlayerFactory
    {
        private readonly IGlobalServiceId _idService;

        public PlayerFactory(IGlobalServiceId idService)
        {
            _idService = idService;
        }

        public PlayerRuntime Create()
        {
            return new PlayerRuntime(_idService.Next());
        }
    }
}
