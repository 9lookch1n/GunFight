using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Ctrl_Canvas : MonoBehaviour
{
    public static Ctrl_Canvas instance;

    public Image[] manaBar;

    public float mana, maxMana;
    public float lerpSpeed;


    public GameObject pressFireLineShot;
    public GameObject targetCicle;

    PhotonView PV;

    private void Start()
    { 
        instance = this;

        PV = GetComponent<PhotonView>();

        mana = maxMana;

        Ctrl_UIStart();

        if (!PV.IsMine)
        {
            manaBar[0].gameObject.SetActive(false);
        }
    }

    public void ManaRegen(float manaRegen)
    {
        if (mana > 0)
        {
            mana -= manaRegen;
        }

    }

    
    public void PressBtt()
    {
        pressFireLineShot.SetActive(true);
        targetCicle.SetActive(true);
        //PV.RPC("RPC_PressBtt", RpcTarget.All);

    }
    public void Ctrl_UIStart()
    {
        pressFireLineShot.SetActive(false);
        targetCicle.SetActive(false);
        //PV.RPC("RPC_Ctrl_UIStart", RpcTarget.All);
    }

    public void RegenMana(float _mana)
    {
        manaBar[0].fillAmount = Mathf.Lerp(manaBar[0].fillAmount, (mana / maxMana), lerpSpeed * 10);

        if (mana > maxMana)
        {
            mana = maxMana;
        }
        if (mana < maxMana)
        {
            mana += _mana;
        }

        lerpSpeed = 15f * Time.deltaTime;

    }

    //ShotGun
    public void StackToStunUI()
    {
        if (mana == 5)
        {
            manaBar[0].enabled = true;
        }
        else if (mana == 4)
        {
            manaBar[1].enabled = true;
        }
        else if (mana == 3)
        {
            manaBar[2].enabled = true;
        }
        else if (mana == 2)
        {
            manaBar[3].enabled = true;
        }
        else if (mana == 1)
        {
            manaBar[4].enabled = true;
        }
        else if (mana == 0)
        {
            manaBar[0].enabled = false;
            manaBar[1].enabled = false;
            manaBar[2].enabled = false;
            manaBar[3].enabled = false;
            manaBar[4].enabled = false;
        }
    }
}
