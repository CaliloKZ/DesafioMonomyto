using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class CurrentRoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private string m_sceneToLoad;
    [SerializeField] private GameEvent m_onHostLeft;

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        m_onHostLeft.Raise();
        if (PhotonNetwork.LocalPlayer == newMasterClient)
            PhotonNetwork.DestroyAll();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene(m_sceneToLoad);
    }
}
