using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_BulletShotGun : Bullet
{
    public CTRL_Player ctrl_Player;
    private void Start()
    {
        ctrl_Player = GetComponent<CTRL_Player>();
    }

    private void FixedUpdate()
    {

        timeFalse += Time.fixedDeltaTime;
        DestroyBullet();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("-HP");

            timeFalse = 0.0f;
            gameObject.SetActive(false);
        }
    }
}
