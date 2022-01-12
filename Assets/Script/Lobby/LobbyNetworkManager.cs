using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyNetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField _roomInput;
    [SerializeField] private RoomItemUI _roomItemUIPrefab;
    [SerializeField] private Transform _roomListParent;

    [SerializeField] private Text _statusField;
    [SerializeField] private GameObject _leaveRoomButton;

    private List<RoomItemUI> _roomList = new List<RoomItemUI>();
    
    void Start()
    {
        Connect();
    }

    #region PhotonCallbacks
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateRoomList(roomList);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected");
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Join Lobby");
    }
    public override void OnJoinedRoom()
    {
        _statusField.text = "Joined" + PhotonNetwork.CurrentRoom.Name;
        Debug.Log("Join Room : " + PhotonNetwork.CurrentRoom.Name);
    }
    public override void OnLeftRoom()
    {
        _statusField.text = "LOBBY";
       Debug.Log("Left Room : ");
    }
    #endregion

    private void Connect()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(0, 5000);
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void UpdateRoomList(List<RoomInfo> roomList)
    {
        print("UpdateRoomList");
        //clear the current list of rooms
        for (int i = 0; i < _roomList.Count; i++)
        {
            Destroy(_roomList[i].gameObject);
        }

        _roomList.Clear();

        //generate a new list with update info
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].PlayerCount == 0) { continue; }

            RoomItemUI newRoomItem = Instantiate(_roomItemUIPrefab);
            newRoomItem.LobbyNetworkParent = this;
            newRoomItem.transform.SetParent(_roomListParent);
            _roomList.Add(newRoomItem);
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(_roomInput.text) == false)
        {
            PhotonNetwork.CreateRoom(_roomInput.text, new RoomOptions() { MaxPlayers = 6 }, null);
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}

