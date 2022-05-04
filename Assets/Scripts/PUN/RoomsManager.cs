using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class RoomsManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField m_createRoomInputField;

    [SerializeField] private TextMeshProUGUI m_errorText;
    [SerializeField] private TextMeshProUGUI m_roomNameText;

    [SerializeField] private Transform m_roomListContent;
    [SerializeField] private GameObject m_roomListItemPrefab;

    [SerializeField] private Transform m_playerListContent;
    [SerializeField] private GameObject m_playerListItemPrefab;

    [SerializeField] private GameObject m_startBT;

    [SerializeField] private List<GameObject> m_roomItems = new List<GameObject>();


    public void CreateRoom()
    {
        if (string.IsNullOrWhiteSpace(m_createRoomInputField.text))
            return;

        if(PhotonNetwork.CountOfRooms > 6)
        {
            m_errorText.text = ("Room creation failed: maximum amount reached.");
            MainMenuManager.OpenPanel(LobbyPanels.Error);
        }
        else
        {
            PhotonNetwork.CreateRoom(m_createRoomInputField.text);
            MainMenuManager.OpenPanel(LobbyPanels.Loading);
        }
    }

    public static void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MainMenuManager.OpenPanel(LobbyPanels.Loading);     
    }

 

    public override void OnJoinedRoom()
    {
        m_roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        MainMenuManager.OpenPanel(LobbyPanels.Room);
        Player[] _playerList = PhotonNetwork.PlayerList;

        for (int i = 0; i < _playerList.Length; i++)
        {
            Instantiate(m_playerListItemPrefab, m_playerListContent).GetComponent<PlayerListItem>().SetUp(_playerList[i]);
        }

        m_startBT.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        m_startBT.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        m_errorText.text = ("Room creation failed: " + message);
        MainMenuManager.OpenPanel(LobbyPanels.Error);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MainMenuManager.OpenPanel(LobbyPanels.Loading);
    }

    public override void OnLeftRoom()
    {
        MainMenuManager.OpenPanel(LobbyPanels.MainMenu);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform child in m_roomListContent)
        {
            child.gameObject.SetActive(false);
        }

        for(int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (!info.RemovedFromList)
            {
                m_roomItems[i].SetActive(true);
                m_roomItems[i].GetComponent<RoomListItem>().SetUp(info);
            }            
            Debug.Log("ItemCreated");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(m_playerListItemPrefab, m_playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

}
