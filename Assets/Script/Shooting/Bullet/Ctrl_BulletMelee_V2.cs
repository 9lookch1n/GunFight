using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using Photon.Pun.UtilityScripts;

public class Ctrl_BulletMelee : Bullet
{
    public PhotonTransformView PTV;

    private void Start()
    {
        PTV = GetComponent<PhotonTransformView>();
    }
    private void FixedUpdate()
    {
        timeFalse += Time.fixedDeltaTime;
        DestroyBullet();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Debug.Log("Monster");
            //other.transform.GetComponent<Ctrl_Monster>().HP -= player.transform.GetComponent<Ctrl_FireMelee>()._damage;
            if (other.transform.GetComponent<Ctrl_Monster>().HP <= 0)
            {
                //player.transform.GetComponent<Ctrl_FireMelee>()._damage += 20;
            }

        }
        if (other.gameObject.tag == "Player")
        {
            //TakeDamage
            if (other.transform.GetComponent<Health>().currentHealth <= 10)
            {
                player.transform.GetComponent<Ctrl_FireMelee>().kill ++;
                player.transform.GetComponent<Health>().addScore();
            }
    
            //other.transform.GetComponent<Health>().Takedamage(player.transform.GetComponent<Ctrl_FireMelee>()._damage);

            //Add StackSlow to Player
            other.transform.GetComponent<CTRL_Player>().stackSlow += 1;

            Debug.Log(GetComponent<CTRL_Player>().stackSlow);

            timeFalse = 0.0f;
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Wall")
        {
            Debug.Log("Wall");
            timeFalse = 0.0f;
            Destroy(gameObject);
        }
    }

}
