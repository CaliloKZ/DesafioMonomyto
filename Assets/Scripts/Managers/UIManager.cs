using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_weaponSelectHighlights = new List<GameObject>();
    [SerializeField] private GameObject m_currentSelectedHighlight;

    public void ChangeWeaponHighlight(int index)
    {
        m_currentSelectedHighlight.SetActive(false);
        m_currentSelectedHighlight = m_weaponSelectHighlights[index];
        m_currentSelectedHighlight.SetActive(true);
    }
}
