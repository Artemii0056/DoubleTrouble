using System.Collections.Generic;

namespace Game.Characters.Enemy.Services
{
    public sealed class DamageableRuntimeStore
    {
        private readonly Dictionary<int, IDamageable> _items = new();

        public void Register(IDamageable item) =>
            _items[item.Id] = item;

        public void Unregister(int id) =>
            _items.Remove(id);

        public bool TryGet(int id, out IDamageable item) =>
            _items.TryGetValue(id, out item);
    }
}