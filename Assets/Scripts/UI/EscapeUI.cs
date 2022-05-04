using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EscapeUI : MonoBehaviour
{
    [SerializeField] private GameObject m_container;
    [SerializeField] private GameObject m_backToGameButton;

    [SerializeField] private TextMeshProUGUI m_message;

    public void OnHostLeft()
    {
        m_message.text = "Host Saiu";
        m_backToGameButton.SetActive(false);
        OpenPanel();
    }

    public void OpenPanel() => m_container.SetActive(true);

    public void ClosePanel() => m_container.SetActive(false);
}
