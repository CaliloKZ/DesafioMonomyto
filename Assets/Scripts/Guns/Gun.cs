using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public abstract class Gun : MonoBehaviour
{
    private PlayerInputActions m_playerInputActions;

    [SerializeField] protected int _damage;
    [SerializeField] protected float _bulletSpeed;
    [SerializeField] protected GameObject _bulletPrefab;

    [SerializeField] protected Transform _firePoint;
    [SerializeField] protected float _fireRate;
    protected float _nextTimeToFire = 0f;

    protected ObjectPool<GameObject> _bulletPool;

    [SerializeField] protected int _poolDefaultCapacity = 10;
    [SerializeField] protected int _poolMaxCapacity = 100;

    protected bool _canShoot = true;
    protected bool _isShooting = false;

    protected virtual void Awake()
    {
        m_playerInputActions = new PlayerInputActions();
    }

    protected virtual void OnEnable()
    {
        m_playerInputActions.Gun.Enable();
        m_playerInputActions.Gun.Shoot.started += ShootInput;
        m_playerInputActions.Gun.Shoot.canceled += ShootInput;
    }

    protected virtual void OnDisable()
    {
        _isShooting = false;
        m_playerInputActions.Gun.Shoot.started -= ShootInput;
        m_playerInputActions.Gun.Shoot.canceled -= ShootInput;
        m_playerInputActions.Gun.Disable();
        _bulletPool.Dispose();
    }

    protected virtual void Start()
    {
        _bulletPool = new ObjectPool<GameObject>(() => {
            return Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        }, bullet => {
            PoolOnGet(bullet);
        }, bullet => {
            PoolOnRelease(bullet);
        }, bullet => {
            PoolOnDestroy(bullet);
        }, false, _poolDefaultCapacity, _poolMaxCapacity);
    }

    #region PoolMethods
    protected virtual void PoolOnGet(GameObject obj)
    {
        obj.SetActive(true);
    }

    protected virtual void PoolOnRelease(GameObject obj)
    {
        obj.SetActive(false);
    }

    protected virtual void PoolOnDestroy(GameObject obj)
    {
        Destroy(obj);
    }

    #endregion

    protected virtual void Update()
    {
        if(_isShooting && Time.time >= _nextTimeToFire)
        {
            Shoot();
        }
    }

    protected virtual void ShootInput(InputAction.CallbackContext context)
    {
        if (!_canShoot)
            return;

        if (context.started)
            _isShooting = true;
        else if (context.canceled)
            _isShooting = false;
    }

    protected virtual void Shoot()
    {
        _nextTimeToFire = Time.time + _fireRate;

        var bullet = _bulletPool.Get();
        bullet.transform.position = _firePoint.position;
        bullet.transform.rotation = _firePoint.rotation;
        bullet.GetComponent<Rigidbody2D>().AddForce(_firePoint.right * _bulletSpeed, ForceMode2D.Impulse);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDamage(_damage);
        bulletScript.Init(KillBullet);

    }

    protected virtual void KillBullet(Bullet bullet)
    {
        _bulletPool.Release(bullet.gameObject);
    }

   
}
