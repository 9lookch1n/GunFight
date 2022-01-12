using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ctrl_FireSniper : MonoBehaviour
{
    public Vector3 posBullet;

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
        posBullet = bulletPosition[0].position - playerPosition.position;
        posBullet.y = 0;
    }

    public void Shooting()
    {
        Ctrl_Canvas.instance.PressBtt();
        if (canPress)
        {
            StartCoroutine(DelayFireSniper());

            //Delay sec to Reload
            if (amouthShoot < maxLimit)
            {
                Ctrl_Canvas.instance.ManaRegen(100f);
                Invoke("Reload", 3f);
            }
        }
    }
    IEnumerator DelayFireSniper()
    {
        amouthShoot -= 1;
        canPress = false;
        GameObject obj = PhotonNetwork.Instantiate(bulletprefab.name, bulletPosition[0].position, Quaternion.identity, 0);
        obj.GetComponent<Rigidbody>().AddForce(posBullet.normalized * 10, ForceMode.Impulse);
        obj.name = Random.Range(0, 1).ToString();

        yield return new WaitForSeconds(.2f);

    }
    void Reload()
    {
        if (amouthShoot == 0)
        {
            amouthShoot += 1;
        }
        canPress = true;
    }
}
