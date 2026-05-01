using Game.Characters.Player.Scripts;
using UnityEngine;

namespace Game.Combat.Targeting
{
    public sealed class PlayerTargetService
    {
        private readonly PhysicsTargetScanner _scanner;
        private readonly Transform _player;

        private int _lastRefreshFrame = -1;
        private IAimTarget _currentTarget;

        public PlayerTargetService(
            PhysicsTargetScanner scanner,
            IPlayerTransform playerTransform)
        {
            _scanner = scanner;
            _player = playerTransform.Transform;
        }

        public IAimTarget CurrentTarget
        {
            get
            {
                RefreshIfNeeded();
                return _currentTarget;
            }
        }

        public Vector3 PlayerPosition => _player.position;

        private void RefreshIfNeeded()
        {
            int frame = Time.frameCount;

            if (_lastRefreshFrame == frame)
                return;

            _lastRefreshFrame = frame;
            _currentTarget = _scanner.FindNearestTarget(_player.position);
        }
    }
}
