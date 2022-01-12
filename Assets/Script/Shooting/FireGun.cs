using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireGun : MonoBehaviour
{
    public Image[] manaBar;

    public float  mana,maxMana;
    public float lerpSpeed;

    public GameObject pressFireShot;
    public GameObject targetCicle;
    private void Start()
    {
        mana = maxMana;
    }
    private void Update()
    {

    }
    public void Damage(float damagePoints)
    {
        if (mana > 0)
        {
            mana -= damagePoints;
        }

    }
}
