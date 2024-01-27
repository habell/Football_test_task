using System;
using Mirror;
using UnityEngine;

public class TurretController3D : NetworkBehaviour
{
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private Transform baseTurret;
    [SerializeField]
    private Transform turretBarrel;

    private bool _isSpawned;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        camera.enabled = true;
        _isSpawned = true;
    }
    
    private void Awake() => 
        camera.enabled = false;

    private void Update()
    {
        if (!isLocalPlayer || !_isSpawned) return;

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetDirection = hit.point - transform.position;
            Vector3 horizontalDirection = targetDirection;
            horizontalDirection.y = 0;

            Quaternion horizontalRotation = Quaternion.LookRotation(horizontalDirection);
            baseTurret.rotation = Quaternion.Slerp(baseTurret.rotation, horizontalRotation, Time.deltaTime * 5f);

            float angle = Mathf.Atan2(targetDirection.y, horizontalDirection.magnitude) * Mathf.Rad2Deg;
            angle = -angle;
            Quaternion verticalRotation = Quaternion.Euler(angle, turretBarrel.eulerAngles.y, 0);
            turretBarrel.rotation = Quaternion.Slerp(turretBarrel.rotation, verticalRotation, Time.deltaTime * 5f);
        }
    }
}