using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;


public class PlayerWeaponSelection : MonoBehaviour
{
    public static event Action<Weapon> OnWeaponChanged;

    private PlayerInputActions m_playerInputActions;

    private Animator m_playerAnimator;

    [field: SerializeField] public List<Weapon> weaponsList { get; private set; }
    public Weapon selectedWeapon { get; private set; }
    private int m_currentWeaponIndex = 0;

    private PhotonView m_playerPhotonView;

    private void Awake()
    {
        m_playerPhotonView = GetComponent<PhotonView>();
        m_playerAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (!m_playerPhotonView.IsMine)
            return;

        m_playerInputActions = new PlayerInputActions();
        m_playerInputActions.Enable();
        m_playerInputActions.Player.WeaponScroll.performed += SwitchWeaponScroll;
        m_playerInputActions.Player.WeaponKey.performed += SwitchWeaponKey;
        m_playerPhotonView.RPC("EquipWeapon", RpcTarget.All, Weapons.Single);
        //EquipWeapon(weaponsList[0]);
    }

    void SwitchWeaponScroll(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>().y > 0)
        {
            m_currentWeaponIndex++;
            if (m_currentWeaponIndex > weaponsList.Count - 1) 
                m_currentWeaponIndex = 0;

             m_playerPhotonView.RPC("EquipWeapon", RpcTarget.All, (Weapons)m_currentWeaponIndex);
            //EquipWeapon(weaponsList[m_currentWeaponIndex]);
        }
        else if (context.ReadValue<Vector2>().y < 0)
        {
            m_currentWeaponIndex--;
            if (m_currentWeaponIndex < 0) 
                m_currentWeaponIndex = weaponsList.Count - 1;

             m_playerPhotonView.RPC("EquipWeapon", RpcTarget.All, (Weapons)m_currentWeaponIndex);
            //EquipWeapon(weaponsList[m_currentWeaponIndex]);
        }
    }

    void SwitchWeaponKey(InputAction.CallbackContext context)
    {
        var _weapon = (Weapons)context.ReadValue<float>();

        if (_weapon != selectedWeapon.GetWeaponType())
            m_playerPhotonView.RPC("EquipWeapon", RpcTarget.All, _weapon);
            //EquipWeapon(_weapon); 
    }

    [PunRPC]
    public void EquipWeapon(Weapons weapon)
    {
        if (selectedWeapon != null)
            selectedWeapon.gameObject.SetActive(false);

        for(int i = 0; i < weaponsList.Count; i++)
        {
            if(weaponsList[i].GetWeaponType() == weapon)
            {
                selectedWeapon = weaponsList[i];
                break;
            }
        }

        selectedWeapon.gameObject.SetActive(true);
        m_playerAnimator.SetInteger("CannonIndex", (int)selectedWeapon.GetWeaponType());

        if (m_playerPhotonView.IsMine)
            OnWeaponChanged(selectedWeapon);
    }


}
