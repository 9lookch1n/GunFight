using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    [SerializeField] private Transform pos;

    PhotonView PV;


    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;

        PV = GetComponent<PhotonView>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;

        PhotonTeamsManager.PlayerLeftTeam += OnPlayerLeftTeam;
        PhotonTeamsManager.PlayerJoinedTeam += OnPlayerJoinedTeam;
    }

    private void OnPlayerLeftTeam(Player player, PhotonTeam team)
    {
        Debug.LogFormat("Player {0} left team {1}", player, team);
    }

    private void OnPlayerJoinedTeam(Player player, PhotonTeam team)
    {
        Debug.LogFormat("Player {0} joined team {1}", player, team);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;

        PhotonTeamsManager.PlayerLeftTeam -= OnPlayerLeftTeam;
        PhotonTeamsManager.PlayerJoinedTeam -= OnPlayerJoinedTeam;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 1)
        {
            PhotonNetwork.Instantiate("PhotonPrefabs/PlayerManager", pos.transform.position, Quaternion.identity, 0);
        }
    }
}
