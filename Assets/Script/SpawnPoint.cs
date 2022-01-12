using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject spawnPoint;

    private void Awake()
    {
        spawnPoint.SetActive(false);
    }
}
