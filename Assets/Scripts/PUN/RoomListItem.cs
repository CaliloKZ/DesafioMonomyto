using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_roomNameText;

    private RoomInfo m_roomInfo;

    public void SetUp(RoomInfo info)
    {
        m_roomInfo = info;
        m_roomNameText.text = info.Name;
    }

    public void OnClick()
    {
        RoomsManager.JoinRoom(m_roomInfo);
    }
}
