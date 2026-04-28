using System.Collections.Generic;

namespace Game.Characters.Enemy.Services
{
    public sealed class DamageableRuntimeRegistry
    {
        private readonly Dictionary<int, IDamageableRuntime> _items = new();

        public void Register(IDamageableRuntime item) =>
            _items[item.Id] = item;

        public void Unregister(int id) =>
            _items.Remove(id);

        public bool TryGet(int id, out IDamageableRuntime item) =>
            _items.TryGetValue(id, out item);
    }
}