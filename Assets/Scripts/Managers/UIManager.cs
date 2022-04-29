using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private List<GameObject> m_weaponSelectHighlights = new List<GameObject>();
    [SerializeField] private GameObject m_currentSelectedHighlight;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Another instance of UIManager was found, destroying gameObject: " + gameObject.name);
            Destroy(gameObject);
        }
    }

    public void ChangeWeaponHighlight(int index)
    {
        m_currentSelectedHighlight.SetActive(false);
        m_currentSelectedHighlight = m_weaponSelectHighlights[index];
        m_currentSelectedHighlight.SetActive(true);
    }
}
