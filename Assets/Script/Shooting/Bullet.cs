using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//New Bullet
public class Bullet : MonoBehaviour
{
    public static Bullet instance;
    public Rigidbody rb;
    public GameObject player;

    public float distance;
    public int Damage;
    public float timeFalse = 0.0f;

    private void Start()
    {
        instance = this;
    }

    //SetActive "false" pooling Bullet
    public virtual void DestroyBullet()
    {
        //Check Time == Distance
        if (timeFalse >= distance)
        {
            timeFalse = 0.0f;
            gameObject.SetActive(false);
        }
    }

}
