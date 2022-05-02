using UnityEngine;
using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private string m_sceneToLoad;
    [SerializeField] private GameObject m_loadingPanel;

    private void Start()
    {
        Debug.Log("Connecting to master...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master!");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby Joined!");
        m_loadingPanel.SetActive(false);
    }


}
