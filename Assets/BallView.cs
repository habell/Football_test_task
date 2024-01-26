using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Sirenix.OdinInspector;
using UnityEngine;

public class BallView : NetworkBehaviour
{
    [SerializeField]
    private Rigidbody _rb;

    [Server] 
    public void BallForce(Transform spawnPosition)
    {
        if(!_rb)
            _rb = GetComponent<Rigidbody>();
        _rb.AddForce(spawnPosition.forward * 500);
        print(spawnPosition.position);
    }
}