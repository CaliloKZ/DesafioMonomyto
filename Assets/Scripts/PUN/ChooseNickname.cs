using UnityEngine;
using TMPro;
using Photon.Pun;

public class ChooseNickname : MonoBehaviour
{
    [SerializeField] private TMP_InputField m_nickInputField;
    
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

        gameObject.SetActive(false);

    }
}
