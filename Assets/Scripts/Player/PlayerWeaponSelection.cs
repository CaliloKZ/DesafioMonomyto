using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerWeaponSelection : MonoBehaviour
{
    public static event Action<Weapon> OnWeaponChanged;

    private PlayerInputActions m_playerInputActions;

    private Animator m_playerAnimator;

    [field: SerializeField] public List<Weapon> weaponsList { get; private set; }
    public Weapon selectedWeapon { get; private set; }
    private int m_currentWeaponIndex = 0;

    private void Awake()
    {
        m_playerAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_playerInputActions = new PlayerInputActions();
        m_playerInputActions.Enable();
        m_playerInputActions.Player.WeaponScroll.performed += SwitchWeaponScroll;
        m_playerInputActions.Player.WeaponKey.performed += SwitchWeaponKey;
        EquipWeapon(weaponsList[0]);
    }

    void SwitchWeaponScroll(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>().y > 0)
        {
            m_currentWeaponIndex++;
            if (m_currentWeaponIndex > weaponsList.Count - 1) m_currentWeaponIndex = 0;
            EquipWeapon(weaponsList[m_currentWeaponIndex]);
        }
        else if (context.ReadValue<Vector2>().y < 0)
        {
            m_currentWeaponIndex--;
            if (m_currentWeaponIndex < 0) m_currentWeaponIndex = weaponsList.Count - 1;
            EquipWeapon(weaponsList[m_currentWeaponIndex]);
        }
    }

    void SwitchWeaponKey(InputAction.CallbackContext context)
    {
        var _weapon = weaponsList[(int)context.ReadValue<float>()];

        if(_weapon != selectedWeapon)
            EquipWeapon(_weapon); 
    }

    public void EquipWeapon(Weapon weapon)
    {
        if(selectedWeapon != null)
            selectedWeapon.gameObject.SetActive(false);

        selectedWeapon = weapon;
        selectedWeapon.gameObject.SetActive(true);

        OnWeaponChanged(selectedWeapon);

        m_playerAnimator.SetInteger("CannonIndex", (int)selectedWeapon.GetWeaponType());
    }

}
