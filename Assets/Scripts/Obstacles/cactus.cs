using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cactus : MovingObject
{
    void OnTriggerEnter(Collider hit)
    {
        if(!hit.gameObject.CompareTag("Player")) return;
        
        playerMove.instance.Die();
    }
}
