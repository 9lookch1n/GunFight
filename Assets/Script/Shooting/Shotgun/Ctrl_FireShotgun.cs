using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ctrl_FireShotgun : MonoBehaviour
{
    public Vector3[] posBullet;

    public Transform[] bulletPosition;
    public Transform playerPosition;

    public GameObject bulletprefab;

    public int amouthShoot;
    public int maxLimit;
    public bool canPress = true;

    private void Update()
    {
        positionPlayer();
    }
    void positionPlayer()
    {
        for (int i = 0; i < 3; i++)
        {
            posBullet[i] = bulletPosition[i].position - playerPosition.position;
            posBullet[i] = bulletPosition[i].position - playerPosition.position;
            posBullet[i] = bulletPosition[i].position - playerPosition.position;
        }
    }

    public void Shooting()
    {
        if (canPress)
        {
            Ctrl_Canvas.instance.mana -= 1;
            Ctrl_Canvas.instance.StackToStunUI();

            for (int i = 0; i < maxLimit; i++)
            {
                amouthShoot -= 1;
                canPress = false;

                GameObject obj = PhotonNetwork.Instantiate(bulletprefab.name, bulletPosition[i].position, Quaternion.identity, 0);

                if (Ctrl_Canvas.instance.mana == 0)
                {
                    obj.GetComponent<Renderer>().material.color = new Color(2, 1, 0);
                    Ctrl_Canvas.instance.mana = 6;
                }

                obj.GetComponent<Rigidbody>().AddForce(posBullet[i].normalized * 10, ForceMode.Impulse);
                obj.name = Random.Range(0, 3).ToString();
                obj.SetActive(true);

            }

            //Delay sec to Reload
            if (amouthShoot < maxLimit)
            {
                Invoke("Reload", 1.2f);

            }
        }
    }
    void Reload()
    {
        if (amouthShoot == 0)
        {
            amouthShoot += 3;
        }
        canPress = true;
    }

}
