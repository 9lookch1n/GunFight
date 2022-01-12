using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Photon.Pun;

public class Ctrl_FireMelee_2 : MonoBehaviour
{
    public static Ctrl_FireMelee_2 instance;
    [SerializeField] private Transform bulletPosition;
    bool CantPress = true;
    public int amouthShoot;

    int maxLimit = 3;
    public GameObject[] ShootLimit;

    List<GameObject> bullet = new List<GameObject>();

    PhotonView PV;
    private void Start()
    {
        PV = GetComponent<PhotonView>();

        instance = this;
        bullet = Ctrl_Melee_2.instance.GetPoolingObj();
        amouthShoot = 3;

        bulletPosition.name = Random.Range(0, 10).ToString();

    }
    public void FireMelee()
    {
        if(PV.IsMine)
        {
            if (CantPress)
            {
                if (amouthShoot >= maxLimit)
                {
                    CantPress = false;
                    PV.RPC("DelayFireMelee", RpcTarget.All);

                    if (amouthShoot < maxLimit)
                    {
                        Invoke("Reload", 2f);
                        //PV.RPC("Reload", RpcTarget.All);
                    }
                }
            }
        }
    }

    [PunRPC]
    IEnumerator DelayFireMelee()
    {
        foreach (GameObject bullets in bullet)
        {
            if (bullets != null)
            {
                amouthShoot -= 1;
                PV.RPC("UICtrlfalse", RpcTarget.All);
                bullets.transform.position = bulletPosition.transform.position;
                Debug.Log(bulletPosition.parent.gameObject.name);
                Debug.Log(bulletPosition.name);
                bullets.SetActive(true);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    [PunRPC]
    void Reload()
    {
        for (int i = 0; i < maxLimit; i++)
        {
            ShootLimit[i].SetActive(true);
            amouthShoot += 1;
        }
        CantPress = true;
    }
    
    [PunRPC]
    void UICtrlfalse()
    {
        if(PV.IsMine)
        {
            if (amouthShoot == 2)
            {
                ShootLimit[2].SetActive(false);
            }
            else if (amouthShoot == 1)
            {
                ShootLimit[1].SetActive(false);
            }
            else if (amouthShoot == 0)
            {
                ShootLimit[0].SetActive(false);
            }
        }
    }
}