using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Weapons _weaponType;
    public Weapons GetWeaponType()
    {
        return _weaponType;
    }

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

    [SerializeField] protected int _maxAmmo;
    protected int _currentAmmo;

    protected bool _isActive;

    public int[] GetAmmo()
    {
        var _ammo = new int[] { _currentAmmo, _maxAmmo };
        return _ammo;
    }

    protected bool _canShoot = true;
    protected bool _isShooting = false;

    protected virtual void Awake()
    {
        m_playerInputActions = new PlayerInputActions();
        _currentAmmo = _maxAmmo;
    }

    protected virtual void OnEnable()
    {
        _isActive = true;
        m_playerInputActions.Weapon.Enable();
        m_playerInputActions.Weapon.Shoot.started += ShootInput;
        m_playerInputActions.Weapon.Shoot.canceled += ShootInput;
    }

    protected virtual void OnDisable()
    {
        _isActive = false;
        _isShooting = false;
        m_playerInputActions.Weapon.Shoot.started -= ShootInput;
        m_playerInputActions.Weapon.Shoot.canceled -= ShootInput;
        m_playerInputActions.Weapon.Disable();
        _bulletPool.Dispose();
    }

    protected virtual void Start()
    {
        _bulletPool = new ObjectPool<GameObject>(() => {
            return Instantiate(_bulletPrefab);
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

    protected virtual void Shoot() { }

    protected virtual void KillBullet(Bullet bullet)
    {
        _bulletPool.Release(bullet.gameObject);
    }

    public virtual void AmmoPickup(int amount)
    {
        _currentAmmo += amount;
        if(_currentAmmo > _maxAmmo)
            _currentAmmo = _maxAmmo;

        if(_isActive)
            AmmoCount.OnCurrentAmmoChange(_currentAmmo);
    }
   
}
