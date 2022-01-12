using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ctrl_Melee : Weapon
{
    public static Ctrl_Melee instance;

    private List<GameObject> poolingObject = new List<GameObject>();

    PhotonView PV;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        PV = GetComponent<PhotonView>();
    }
    public void Start()
    {
        if (PV.IsMine)
        {
            InstantiateBullet();
        }
        
    }
    public override void InstantiateBullet()
    {
        for (int i = 0; i < amouthToPool; i++)
        {
            GameObject obj = PhotonNetwork.Instantiate("PhotonPrefabs/Bullet", transform.position, Quaternion.identity, 0);
            obj.SetActive(false);
            obj.transform.SetParent(GameObject.Find("BulletBox").transform);
            obj.transform.GetComponent<Bullet>().player = this.gameObject; //ส่งผู้เล่นไปที่ Bullet
            poolingObject.Add(obj);
        }
    }
    public override List<GameObject> GetPoolingObj()
    {
        List<GameObject> bullet = new List<GameObject>();
        for (int i = 0; i < poolingObject.Count; i++)
        {
            if (bullet.Count < maxBullet)
            {
                if (!poolingObject[i].activeInHierarchy)
                {
                    bullet.Add(poolingObject[i]);
                }
            }
        }
        return bullet;
    }
}
