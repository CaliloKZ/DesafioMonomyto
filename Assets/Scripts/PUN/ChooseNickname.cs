using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;

public class ChooseNickname : MonoBehaviour
{
    [SerializeField] private TMP_InputField m_nickInputField;
    [SerializeField] private string m_sceneToLoad;
    
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

        SceneManager.LoadScene(m_sceneToLoad);
    }
}
