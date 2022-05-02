using System.Collections.Generic;
using UnityEngine;

public class SelectedWeaponHighlight : MonoBehaviour
{

    [SerializeField] private List<GameObject> m_weaponHighlights = new List<GameObject>();
    private int m_currentWeaponIndex = 0;

    private void Start() => PlayerWeaponSelection.OnWeaponChanged += ChangeWeaponHighlight;
    private void OnDestroy() => PlayerWeaponSelection.OnWeaponChanged -= ChangeWeaponHighlight;

    public void ChangeWeaponHighlight(Weapon weapon)
    {
        var _weaponindex = (int)weapon.GetWeaponType();
        m_weaponHighlights[m_currentWeaponIndex].SetActive(false);
        m_currentWeaponIndex = _weaponindex;
        m_weaponHighlights[m_currentWeaponIndex].SetActive(true);

    }
}
