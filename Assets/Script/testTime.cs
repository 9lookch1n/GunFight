using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.Text;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class testTime : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text TimeUIText;
    [SerializeField] private float timeToStart = 180.0f;
    [SerializeField] GameObject winner;

    public Animator[] anim;

    int score;

    public GameObject[] map;

    private void Start()
    {
        winner.SetActive(false);
    }
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (timeToStart >= 0.0f)
            {
                timeToStart -= Time.deltaTime;
                photonView.RPC("SetTime", RpcTarget.AllBuffered, timeToStart);
                //photonView.RPC("UpdateHigthScore", RpcTarget.All, 3);
                //UpdateHigthScore(3);
            }
            else if (timeToStart <= 0.0f)
            {

                photonView.RPC("EndGame", RpcTarget.AllBuffered);
            }

            if (timeToStart <= 80.0f)
            {
                anim[0].Play("Map_1");
                anim[1].Play("Map_2");
            }
        }
    }
    [PunRPC]
    public void SetTime(float time)
    {
        if (time > 0.0f)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int second = Mathf.FloorToInt(time - minutes * 60f);

            string textTime = string.Format("{0:0}:{1:00}", minutes, second);

            TimeUIText.text = textTime.ToString();
        }
        else
        {
            //The countdown is over
            TimeUIText.text = ("0:00");
        }
    }
    [PunRPC]
    public void EndGame()
    {
        TimeUIText.text = ("0:00");
        //EndGame
        //winner.SetActive(true);
        if (winner.activeInHierarchy == false)
        {
            photonView.RPC("UpdateHigthScore", RpcTarget.All, 0);
           // UpdateHigthScore(0);
        }
        
        FindObjectOfType<CTRL_Player>().enabled = false;
        FindObjectOfType<Ctrl_FireMelee>().enabled = false;
        FindObjectOfType<CTRL_Player>().joyButton.SetActive(false);
    }
    [PunRPC]
    void UpdateHigthScore(int scoreFull)
    {
        //Get plyer Name

        var playerList = new StringBuilder();
        Player pWin = PhotonNetwork.PlayerList[0];

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            score = p.GetScore();
            if (score > scoreFull)
            {
                pWin = p;
                scoreFull = p.GetScore();
                playerList.Clear();
                playerList.Append("Name: " + pWin.NickName + " Score: " + pWin.GetScore());
                //playerList.Append("Name: " + p.NickName + " Score: " + p.GetScore());
                //winner.SetActive(true);
                //break;
            }
            else if (score == scoreFull)
            {
                playerList.Append("\n"+ "Name: " + p.NickName + " Score: " + p.GetScore() );
            }
        }
        winner.SetActive(true);

        //ScoreBoardUpdate
        string output = playerList.ToString();
        winner.transform.Find("Text").GetComponent<Text>().text = output;
    }
}
