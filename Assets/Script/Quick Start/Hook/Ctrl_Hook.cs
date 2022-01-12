using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Hook : MonoBehaviour
{

    private GameObject player;
    private GameObject enemy;

    LineRenderer lineRenderer;

    bool isHooking;
    bool wasEnemyHooked;
    bool pressHook = true;

    float hookDistance;
    Vector3 originalPosition;

    Rigidbody rb;

    public CTRL_Player ctrl_player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        lineRenderer = GetComponent<LineRenderer>();
        isHooking = false;
        wasEnemyHooked = false;
        hookDistance = 0f;
        rb = GetComponent<Rigidbody>();
        originalPosition = new Vector3(player.transform.position.x + Ctrl_Constans.X_OFFSET
            , player.transform.position.y, player.transform.position.z + Ctrl_Constans.Z_OFFSET);
    }
    private void Start()
    {
        ctrl_player = GetComponent<CTRL_Player>();
    }
    public void PressHook()
    {
        if (!isHooking && !wasEnemyHooked && pressHook)
        {
            StartHooking();
            pressHook = false;
            StartCoroutine(DelayHookShot());
        }
    }
    private void Update()
    {
        originalPosition = new Vector3(player.transform.position.x + Ctrl_Constans.X_OFFSET, player.transform.position.y
            , player.transform.position.z + Ctrl_Constans.Z_OFFSET);
        lineRenderer.SetPosition(0, originalPosition);
        lineRenderer.SetPosition(1, transform.position);

        ReturnHook();
        BringEnemyTowardPlayer();
    }
    IEnumerator DelayHookShot()
    {
        yield return new WaitForSeconds(2f);
        pressHook = true;
    }
    private void StartHooking()
    {
        isHooking = true;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * Ctrl_Constans.HOOK_SPEED);
    }
    private void ReturnHook()
    {
        if (isHooking)
        {
            hookDistance = Vector3.Distance(transform.position,originalPosition);
            if (hookDistance > Ctrl_Constans.MAX_HOOK_DISTANCE || wasEnemyHooked)
            {
                rb.isKinematic = true;
                transform.position = originalPosition;
                isHooking = false;
            }
        }
    }
    private void BringEnemyTowardPlayer()
    {
        if (wasEnemyHooked)
        {
            Vector3 finalPosition = new Vector3(originalPosition.x, enemy.transform.position.y, originalPosition.z + Ctrl_Constans.ENEMY_Z_OFFSET);
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, finalPosition, Ctrl_Constans.MAX_HOOK_DISTANCE);
            wasEnemyHooked = false;
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals("Enemy"))
        {
            wasEnemyHooked = true;
            enemy = col.gameObject;
        }
    }
}
