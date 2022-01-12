using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using Photon.Pun.UtilityScripts;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;

    GameObject player;
    public GameObject spawnPlayer;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if(PV.IsMine)
        {
            CreateController();
            //PV.RPC("CreateController", RpcTarget.All);
        }
    }

    void CreateController()
    {
        Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
        player = PhotonNetwork.Instantiate("PhotonPrefabs/PlayerMelee_V0.6", spawnpoint.position, Quaternion.identity, 0, new object[] { PV.ViewID });
        //player = Instantiate(spawnPlayer, spawnpoint.position, Quaternion.identity);
        //player.transform.GetComponent<CTRL_Player>().players = player;
        //PhotonNetwork.LocalPlayer.JoinTeam("Blue");
        //Debug.Log(PhotonNetwork.LocalPlayer.GetPhotonTeam());
        player.name = Random.Range(0, 100).ToString();

    }

    public void Die()
    {
        //PhotonNetwork.Destroy(player);

        CreateController();
    }
}
