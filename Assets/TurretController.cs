using Mirror;
using UnityEngine;

public class TurretController3D : NetworkBehaviour
{
    void Update()
    {
        if(!isLocalPlayer) return;
        if(!Camera.main) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetDirection = hit.point - transform.position;
            
            targetDirection.y = 0;

            Quaternion lookRotation = Quaternion.LookRotation(targetDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}