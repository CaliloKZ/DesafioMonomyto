using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_currentAmmoText;
    [SerializeField] private TextMeshProUGUI m_maxAmmoText;

    public void ChangeWeapon(int currentAmmo, int maxAmmo)
    {
        m_currentAmmoText.text = (currentAmmo.ToString());
        m_maxAmmoText.text = ($"/{maxAmmo}");
    }

}
