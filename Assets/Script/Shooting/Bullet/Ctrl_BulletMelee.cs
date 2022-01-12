using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using Photon.Pun.UtilityScripts;

public class Ctrl_BulletMelee : Bullet
{
    private void FixedUpdate()
    {
        timeFalse += Time.fixedDeltaTime;
        DestroyBullet();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            //TakeDamage
            if (other.transform.GetComponent<Health>().currentHealth <= 10)
            {
                player.transform.GetComponent<Ctrl_FireMelee>().kill ++;
                player.transform.GetComponent<Health>().addScore();
            }

            other.transform.GetComponent<Health>().Takedamage(10);

            timeFalse = 0.0f;
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Wall")
        {
            timeFalse = 0.0f;
            Destroy(gameObject);
        }
    }
}
