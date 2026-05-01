using Game.Projectiles.Runtime;
using UnityEngine;

namespace Game.Projectiles.View
{
    public sealed class ProjectileView : MonoBehaviour
    {
        private ProjectileRuntime _runtime;

        public int ProjectileId => _runtime?.Id ?? 0;

        public void Init(ProjectileRuntime runtime)
        {
            _runtime = runtime;
            gameObject.SetActive(true);
            Render();
        }

        public void Deactivate()
        {
            _runtime = null;
            gameObject.SetActive(false);
        }

        public void Render()
        {
            if (_runtime == null)
                return;

            transform.position = _runtime.Position;

            if (_runtime.Direction != Vector3.zero)
                transform.forward = _runtime.Direction;
        }
    }
}
