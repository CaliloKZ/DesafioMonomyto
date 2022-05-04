using UnityEngine;
using TMPro;
using Photon.Pun;

public class ChooseNickname : MonoBehaviour
{
    [SerializeField] private TMP_InputField m_nickInputField;
    [SerializeField] private TextMeshProUGUI m_nickNameText;
    
    public void OkButton()
    {
        if (!string.IsNullOrWhiteSpace(m_nickInputField.text))
        {
            PhotonNetwork.NickName = m_nickInputField.text;
        }
        else
        {
            PhotonNetwork.NickName = ($"Player {Random.Range(0, 1000).ToString("0000")}");
        }

        m_nickNameText.text = PhotonNetwork.NickName;
        MainMenuManager.OpenPanel(LobbyPanels.MainMenu);

    }
}
