using UnityEngine;

namespace TestSystem.TestProjectileLogic.Projectiles
{
    public sealed class ProjectileView : MonoBehaviour
    {
        private ProjectileRuntime _runtime;

        public int ProjectileId => _runtime.Id;

        public void Init(ProjectileRuntime runtime)
        {
            _runtime = runtime;
            Render();
        }

        public void Render()
        {
            transform.position = _runtime.Position;

            if (_runtime.Direction != Vector3.zero)
                transform.forward = _runtime.Direction;
        }
    }
}