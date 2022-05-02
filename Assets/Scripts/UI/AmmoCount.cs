using UnityEngine;
using TMPro;

public class AmmoCount : MonoBehaviour
{
    private static TextMeshProUGUI m_currentAmmoText;
    private static TextMeshProUGUI m_maxAmmoText;

    private void Awake()
    {
        m_currentAmmoText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        m_maxAmmoText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void Start() => PlayerWeaponSelection.OnWeaponChanged += WeaponChanged;

    public static void OnCurrentAmmoChange(int currentAmmo) => m_currentAmmoText.text = currentAmmo.ToString();
    public static void OnMaxAmmoChange(int maxAmmo) => m_maxAmmoText.text = ($"/{maxAmmo}");

    public static void WeaponChanged(Weapon weapon)
    {
        int[] _ammo = weapon.GetAmmo();
        OnCurrentAmmoChange(_ammo[0]);
        OnMaxAmmoChange(_ammo[1]);
    }

}
