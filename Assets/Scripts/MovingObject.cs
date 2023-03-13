using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float worldSpeed;
    public Vector3 directionMultiplier;
    public int destroyTime;

    void Start()
    {
        StartCoroutine(DestroyTimer());
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.position -= directionMultiplier * worldSpeed * Time.deltaTime * Time.timeScale;
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
