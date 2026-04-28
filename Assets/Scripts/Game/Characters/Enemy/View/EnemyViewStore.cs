using System.Collections.Generic;

namespace Game.Characters.Enemy.View
{
    public sealed class EnemyViewStore
    {
        private readonly Dictionary<int, EnemyView> _views = new();

        public void Register(int id, EnemyView view)
        {
            _views[id] = view;
        }

        public void Unregister(int id)
        {
            _views.Remove(id);
        }

        public bool TryGet(int id, out EnemyView view)
        {
            return _views.TryGetValue(id, out view);
        }
    }
}