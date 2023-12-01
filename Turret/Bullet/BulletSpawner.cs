using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField]
    private Bullet _bulletPrefab;
    [SerializeField]
    private int _bulletCount = 20;
    [SerializeField]
    private float _bulletForce;
    [SerializeField]

    public ObjectPool<Bullet> _bulletPool;
    public List<Bullet> _activeBullets;

    private Transform _firePoint;
    [SerializeField]
    private string _targetTag;

    private void Start()
    {
        _bulletPool = new ObjectPool<Bullet>(() =>
        {
            return Instantiate(_bulletPrefab);
        }, _bulletPrefab =>
        {
            _bulletPrefab.gameObject.SetActive(true);
        }, _bulletPrefab =>
        {
            _bulletPrefab.gameObject.SetActive(false);
        }, _bulletPrefab =>
        {
            Destroy(_bulletPrefab);
        }, false, 10, _bulletCount);

    }
    public void Spawn(float damage, float bulletForce)
    {
        Bullet bullet = _bulletPool.Get();
        _activeBullets.Add(bullet);
        bullet.transform.SetPositionAndRotation(_firePoint.position, _firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(_firePoint.up * bulletForce, ForceMode2D.Impulse);
        bullet.Init(KillShape, _targetTag, damage);
    }

    private void KillShape(Bullet bullet)
    {
        _bulletPool.Release(bullet);
    }

    public Transform FirePoint { get { return _firePoint; } set { _firePoint = value; } }
    public string TargetTag { get { return _targetTag; } set { _targetTag = value; } }
}
