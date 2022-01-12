using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Ctrl_Monster : MonoBehaviour
{
    public static Ctrl_Monster instance;

    public Ctrl_ReturnMonster ctrl_ReturnMonster;
    public float CloseDistance = 4f;
    public float longDistance = 6f;

    public Image[] buffBar;

    public Transform playerTranform;
    public Transform point;
    //public int speed = 5;

    public GameObject[] player;
    public GameObject monster;

    public float distancePlayer = Mathf.Infinity;

    public int HP = 100;
    public bool checkMonsterDead = false;
    public int amouthRespawn = 0;

    public bool s = false;

    void Start()
    {
        //point = GameObject.FindGameObjectWithTag("Point").transform;
    }
    private void FixedUpdate()
    {
        foreach (Player _player in PhotonNetwork.PlayerList)
        {
            if (_player.IsMasterClient)
            {
                for (int i = 0; i < player.Length; i++)
                {
                    playerTranform = GameObject.FindGameObjectWithTag("Player").transform;
                    player[i] = GameObject.FindGameObjectWithTag("Player");
                    distancePlayer = Vector3.Distance(player[i].transform.position, transform.position);
                }
            }
        }

        DetectPlayer();

    }
    public void FollowPlayer(int speed)
    {
       transform.position = Vector3.MoveTowards(transform.position, playerTranform.position, speed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, GetComponent<Ctrl_BulletMelee>().PTV.transform.position, speed * Time.deltaTime);
    }
    public void BacktoPoint(int speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, point.position, speed * 3 * Time.deltaTime);
        //CloseDistance = 15.0f;

    }
    public void DetectPlayer()
    {
        Debug.Log(HP + "HP");
        if (HP <= 0)
        {
            BacktoPoint(10);
            monster.GetComponent<MeshRenderer>().enabled = false;
            monster.GetComponent<BoxCollider>().enabled = false;

            StartCoroutine(delayHPmonster(5f));
        }
        else if (HP > 0)
        {

            if (distancePlayer <= longDistance )
            {

                FollowPlayer(2);
                Debug.Log("Attack_Player");


            }

            else if (distancePlayer <= CloseDistance)
            {
                //anim Attack-Monster
                Debug.Log("BackPoint");
                BacktoPoint(2);

            }
            else
            {
                BacktoPoint(2);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, CloseDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, longDistance);
    }
    IEnumerator delayHPmonster(float sec)
    {


        if (amouthRespawn <= 3  && checkMonsterDead == false)
        {
            amouthRespawn += 1;

            if (amouthRespawn == 1)
            {
                buffBar[0].GetComponent<Image>().enabled = true;
            }
            else if (amouthRespawn == 2)
            {
                buffBar[1].GetComponent<Image>().enabled = true;
            }
            else if (amouthRespawn == 3)
            {
                buffBar[2].GetComponent<Image>().enabled = true;
            }
            checkMonsterDead = true;


            yield return new WaitForSeconds(sec);

            if (amouthRespawn  != 3)
            {
                HP = 100;
                checkMonsterDead = false;
                monster.GetComponent<MeshRenderer>().enabled = true;
                monster.GetComponent<BoxCollider>().enabled = true;
            }
            else
            {
                monster.SetActive(false);
            }

        }
    }
}
