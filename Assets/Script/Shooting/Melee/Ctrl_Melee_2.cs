using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class Ctrl_Melee_2 : Weapon
{
    public static Ctrl_Melee_2 instance;

    private List<GameObject> poolingObject = new List<GameObject>();

    [SerializeField] private GameObject bulletPrefab;

    Ctrl_FireMelee_2 bulletPosition;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void Start()
    {
        InstantiateBullet();
    }
    public override void InstantiateBullet()
    {
        for (int i = 0; i < amouthToPool; i++)
        {
            //GameObject obj = Instantiate(bulletPrefab,transform);
            GameObject obj = PhotonNetwork.Instantiate("PhotonPrefabs/Bullet",transform.position, Quaternion.identity, 0);
            obj.SetActive(false);
            obj.transform.SetParent(gameObject.transform);
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
