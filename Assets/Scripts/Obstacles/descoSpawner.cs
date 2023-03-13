using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class descoSpawner : MonoBehaviour
{
    public GameObject[] decoration;
    public spawner spawner;
    public int timeSpan;
    public Vector3 deadline;
    public Vector3 spawnDirection;
    public float maxDistance;

    void Start()
    {
        StartCoroutine(WaitDelay());
    }

    void Spawn()
    {
        MovingObject deco = Instantiate(
            decoration[Random.Range(0, decoration.Length)],
            deadline + new Vector3(
                spawnDirection.x * Random.Range(0, maxDistance + 1),
                spawnDirection.y * Random.Range(0, maxDistance + 1),
                spawnDirection.z * Random.Range(0, maxDistance + 1)
            ),
            Quaternion.identity
        ).GetComponent<MovingObject>();

        deco.worldSpeed = spawner.worldSpeed;
        deco.directionMultiplier = spawner.directionMultiplier;
        deco.destroyTime = spawner.obstaclesDestroyTime;


        StartCoroutine(WaitDelay());
    }

    IEnumerator WaitDelay()
    {
        float waitTime = 1;

        if(Random.Range(0, 1) == 0)
        {
            waitTime = timeSpan + Random.Range(-1, 1);
        }

        yield return new WaitForSeconds(waitTime);
        Spawn();
    }
}
