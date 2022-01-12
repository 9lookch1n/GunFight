using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_ReturnMonster : MonoBehaviour
{
    public Ctrl_Monster ctrl_Monster;
    public float CloseDistance = 4f;

    public Transform monsterTranform;
    public GameObject monster;

    public float distanceMonster = Mathf.Infinity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        monsterTranform = GameObject.FindGameObjectWithTag("Monster").transform;
        distanceMonster = Vector3.Distance(monster.transform.position, transform.position);
        DetectMonster();

    }
    public void DetectMonster()
    {
        if (distanceMonster >= CloseDistance)
        {
            ctrl_Monster.BacktoPoint(10);
            Debug.Log("Back_1");
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, CloseDistance);
    }
}
