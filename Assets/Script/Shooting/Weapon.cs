using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Weapon : MonoBehaviour
{
    public int maxBullet;
    public int amouthToPool;
    public virtual List<GameObject> GetPoolingObj()
    {
        return null;
    }
    public virtual void InstantiateBullet()
    {

    }

}
