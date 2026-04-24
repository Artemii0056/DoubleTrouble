using ShootSystem.Scripts;
using UnityEngine;

public class WeaponShooter : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 3f;

    private float _nextShotTime;

    public void TryShoot(ITargetable target)
    {
        Debug.Log("TryShoot");
        
        if (target == null)
            return;

        if (Time.time < _nextShotTime)
            return;

        _nextShotTime = Time.time + 1f / fireRate;

        Vector3 direction = (target.AimPoint.position - firePoint.position).normalized;

        Projectile projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.LookRotation(direction)
        );

        projectile.Launch(direction);
    }
}