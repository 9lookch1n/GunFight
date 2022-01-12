using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Ctrl_FireMelee : MonoBehaviour
{
    Vector3 posBullet;

    public Transform[] bulletPosition;
    public Transform playerPosition;

    public GameObject bulletprefab;

    public int amouthShoot;
    public int maxLimit;
    public bool canPress = true;
 
    public Text killText;
    public int kill;

    CTRL_Animation anim;
    PhotonView PV;
    private void Start()
    {
        PV = GetComponent<PhotonView>();
        anim = GetComponent<CTRL_Animation>();
        killText.text = kill.ToString();

        if (!PV.IsMine)
        {
            killText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        positionPlayer();

        if (PV.IsMine)
        {
            GetComponent<Ctrl_Canvas>().RegenMana(50f * Time.deltaTime);
            killText.text = kill.ToString();
        }
    }
    void positionPlayer()
    {
        posBullet = bulletPosition[0].position - playerPosition.position;
        posBullet.y = 0;
    }
        
    public void Shooting()
    {
        if (PV.IsMine)
        {
            if (canPress)
            {

                GetComponent<CTRL_Player>().isShooting = true;
                anim.Shoot();
                //StartCoroutine(DelayFireMelee());
                PV.RPC("DelayFireMelee", RpcTarget.All);
                
                //Delay sec to Reload
                if (amouthShoot < maxLimit)
                {
                    GetComponent<Ctrl_Canvas>().ManaRegen(100f);
                    Invoke("Reload", 2f);
                }
            }
        }
        
    }

    [PunRPC]
    IEnumerator DelayFireMelee()
    {
        for (int i = 0; i < 3; i++)
        {
            amouthShoot -= 1;
            canPress = false;
            GameObject obj = Instantiate(bulletprefab, bulletPosition[0].position, Quaternion.identity);
            obj.transform.GetComponent<Bullet>().player = this.gameObject;
            obj.transform.SetParent(GameObject.Find("BulletBox").transform);
            obj.GetComponent<Rigidbody>().AddForce(posBullet.normalized * 10, ForceMode.Impulse);
            obj.name = Random.Range(0, 3).ToString();

            yield return new WaitForSeconds(.4f);
        }
    }
    void Reload()
    {
        if (amouthShoot <= 0)
        {
            amouthShoot += 3;
        }
        canPress = true;
        GetComponent<CTRL_Player>().isShooting = false;
    }
}

