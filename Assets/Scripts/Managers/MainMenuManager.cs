using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class MainMenuManager : MonoBehaviour
{
    private static List<GameObject> m_lobbyPanels = new List<GameObject>();
    private static GameObject m_currentOpenPanel;

    [SerializeField] private TextMeshProUGUI m_playerNameText;

    private void Awake()
    {
        m_lobbyPanels.Clear();
        foreach (Transform child in transform)
        {
            AddPanelsToList(child.gameObject);
        }
    }

    private static void AddPanelsToList(GameObject panel) => m_lobbyPanels.Add(panel);

    public void OpenPanel(GameObject panel)
    {
        if (m_currentOpenPanel == null)
            m_currentOpenPanel = m_lobbyPanels[0];

        m_currentOpenPanel.SetActive(false);
        m_currentOpenPanel = panel;
        m_currentOpenPanel.SetActive(true);
    }

    public static void OpenPanel(LobbyPanels panel)
    {
        if (m_currentOpenPanel == null)
            m_currentOpenPanel = m_lobbyPanels[(int)LobbyPanels.Loading];

        m_currentOpenPanel.SetActive(false);
        m_currentOpenPanel = m_lobbyPanels[(int)panel];
        m_currentOpenPanel.SetActive(true);
    }

    public static void OnGameStart() => m_currentOpenPanel = null;
    public void QuitGame()
    {
        Application.Quit();
    }
}
