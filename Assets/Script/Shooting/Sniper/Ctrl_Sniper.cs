using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Sniper : Weapon
{
    public static Ctrl_Sniper instance;

    private List<GameObject> poolingObject = new List<GameObject>();

    [SerializeField] private GameObject bulletPrefab;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void Start()
    {
        maxBullet = 1;
        amouthToPool = 1;
        InstantiateBullet();
    }
    public override void InstantiateBullet()
    {
        for (int i = 0; i < amouthToPool; i++)
        {
            GameObject obj = Instantiate(bulletPrefab,transform);
            obj.SetActive(false);
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
