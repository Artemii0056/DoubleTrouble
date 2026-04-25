using System.Collections.Generic;
using UnityEngine;

namespace TestSystem.TestProjectaileLogic
{
    public class ProjectileRuntime
    {
        public int Id;
        public int OwnerId;

        public Vector3 Position;
        public Vector3 Direction;
        public float Speed;
        public float Radius;
        public float Lifetime;

        public bool IsAlive = true;

        public int PierceLeft;
        public int RicochetLeft;

        public readonly HashSet<int> HitTargetIds = new();
        public readonly List<IProjectileEffect> Effects = new();
    }
}