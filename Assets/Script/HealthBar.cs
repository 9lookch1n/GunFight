using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image health_Bar;

    public void SetMaxHealth(int health)
    {
        health_Bar.fillAmount = Mathf.Lerp(health_Bar.fillAmount, health / 2, health * 10);
        //slider.maxValue = health;
        //slider.value = health;
    }
    public void SetHealth(int health)
    {
        health_Bar.fillAmount = health;
    }
}
