using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Shotgun : Weapon
{
    public static Ctrl_Shotgun instance;

    private List<GameObject> poolingObject = new List<GameObject>();

    [SerializeField] private GameObject bulletPrefab;

    public Transform parrent;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void Start()
    {
        maxBullet = 3;
        amouthToPool = 3;
        InstantiateBullet();
    }
    public override void InstantiateBullet()
    {

        for (int i = 0; i < amouthToPool; i++)
        {
            GameObject obj = Instantiate(bulletPrefab, transform);
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
