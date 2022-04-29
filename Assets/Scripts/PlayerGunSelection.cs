using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerGunSelection : MonoBehaviour
{
    private PlayerInputActions m_playerInputActions;

    private Animator m_playerAnimator;

    [SerializeField] private List<GameObject> m_gunsList;
    private int m_currentGunIndex = 0;
    private Guns m_selectedGun = 0;

    [SerializeField] private List<Guns> m_obtainedGuns = new List<Guns>();

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
    }

    void SwitchWeaponScroll(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>().y > 0)
        {
            m_currentGunIndex++;
            if (m_currentGunIndex > m_obtainedGuns.Count - 1) m_currentGunIndex = 0;
            EquipGun((Guns)m_currentGunIndex);
        }
        else if (context.ReadValue<Vector2>().y < 0)
        {
            m_currentGunIndex--;
            if (m_currentGunIndex < 0) m_currentGunIndex = m_obtainedGuns.Count - 1;
            EquipGun((Guns)m_currentGunIndex);
        }
    }

    void SwitchWeaponKey(InputAction.CallbackContext context)
    {
        float _gunIndex = context.ReadValue<float>();

        if((Guns)_gunIndex != m_selectedGun)
            EquipGun((Guns)_gunIndex);
  
    }

    public void EquipGun(Guns gun)
    {
        if (!m_obtainedGuns.Contains(gun))
            return;

        m_gunsList[(int)m_selectedGun].SetActive(false);
        m_gunsList[(int)gun].SetActive(true);
        m_selectedGun = gun;
        m_playerAnimator.SetInteger("CannonIndex", (int)gun);
       // UIManager.instance.ChangeWeaponHighlight((int)gun);

    }

}
