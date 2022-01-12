using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    //Room info
    public static PhotonRoom room;
    private PhotonView PV;

    //public bool isGameLoaded;
    public int currentScene;
    public int multiplayerScene;
    public Transform posSpawn;

    [SerializeField] private GameObject[] player = new GameObject[3];

    //Player info
    //Player[] photonPlayer;
    //public int playerInRoom;
    //public int myNumberInRoom;

    //public int playersInRoom;

    public void Awake()
    {
        if (PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
        }
        else
        {
            if (PhotonRoom.room != this)
            {
                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
        PV = GetComponent<PhotonView>();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("We are now in a roon");
        //photonPlayer = PhotonNetwork.PlayerList;
        //playerInRoom = photonPlayer.Length;
        // myNumberInRoom = playerInRoom;
        //PhotonNetwork.NickName = myNumberInRoom.ToString();
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        StaerGame();
    }
    void StaerGame()
    {
        PhotonNetwork.LoadLevel(multiplayerScene);
    }


    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if (currentScene == multiplayerScene)
        {
            CreatPlayer();
        }
    }
    private void CreatPlayer()
    {
         PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"),posSpawn.transform.position, Quaternion.identity,0);
    }
}
