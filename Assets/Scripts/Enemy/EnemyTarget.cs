using System;
using DefaultNamespace;
using ShootSystem.Scripts;
using UnityEngine;

namespace Enemy
{
    public class EnemyTarget : MonoBehaviour, ITargetable
    {
        [SerializeField] private Transform aimPoint;

        private Health _health;

        private void OnEnable()
        {
            _health = new Health();
        }

        public Transform AimPoint => aimPoint != null ? aimPoint : transform;
        public bool IsAlive => _health.IsAlive;
    }
}