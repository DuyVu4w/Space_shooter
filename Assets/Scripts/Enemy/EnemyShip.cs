using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [Header("Shooting Setup")]
    public float minFireInterval = 5f;
    public float maxFireInterval = 15f; 
    public string bulletPoolTag = "EnemyBullet";
    public Transform firePoint;

    private float fireTimer;

    void Start()
    {
        SetNextFireTime();
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f)
        {
            Fire();
            SetNextFireTime();
        }
    }

    private void Fire()
    {
        if (firePoint != null)
        {
            GameObject bullet = ObjectPooler.Instance.SpawnFromPool(bulletPoolTag);
            if (bullet != null) 
            {
                bullet.transform.position = firePoint.position;
                bullet.transform.rotation = firePoint.rotation * Quaternion.Euler(0, 180f, 0);
            }
        }
    }

    private void SetNextFireTime()
    {
        fireTimer = Random.Range(minFireInterval, maxFireInterval);
    }
}
