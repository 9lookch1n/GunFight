using UnityEngine;
using UnityEngine.UI;

public class RoomItemUI : MonoBehaviour
{
    public LobbyNetworkManager LobbyNetworkParent;
    [SerializeField] private Text _roomName;

    public void SetName(string roomName)
    {
        _roomName.text = roomName;
    }
    public void OnjoinPressed()
    {
        LobbyNetworkParent.JoinRoom(_roomName.text);
    }
}
