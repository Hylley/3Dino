using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject[] obstacles;
    public Vector3 spawnPosition;
    public Vector3 directionMultiplier;
    public float timeSpanMinimum, timeSpanMaximum;
    [Space(7)]
    public ground ground;
    public float worldSpeed;
    public float worldSpeedIncrementRatio;
    [Space(7)]
    public int obstaclesDestroyTime = 10;

    void Start()
    {
        Spawn();
    }

    void Update()
    {
        worldSpeed += worldSpeedIncrementRatio * Time.deltaTime * Time.timeScale;
        //ground.scrollSpeed = worldSpeed;
        
        // if(timeSpanMinimum >= .1)
        // {
        //     timeSpanMinimum -= (worldSpeedIncrementRatio / 5) * Time.deltaTime * Time.timeScale;
        // }
    }

    void Spawn()
    {
        MovingObject obstacle = Instantiate(obstacles[Random.Range(0, obstacles.Length)], spawnPosition, Quaternion.identity).GetComponent<MovingObject>();
        obstacle.worldSpeed = worldSpeed;
        obstacle.directionMultiplier = directionMultiplier;
        obstacle.destroyTime = obstaclesDestroyTime;


        StartCoroutine(WaitDelay());
    }

    IEnumerator WaitDelay()
    {
        yield return new WaitForSeconds(Random.Range(timeSpanMinimum, timeSpanMaximum + 1));
        Spawn();
    }
}
