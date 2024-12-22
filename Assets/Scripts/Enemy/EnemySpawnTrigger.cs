using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject enemySpawns;
    [SerializeField] private KeypadDoor entranceDoor;

    bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            enemySpawns.SetActive(true);
        }
        entranceDoor.CloseDoor();
    }
}
