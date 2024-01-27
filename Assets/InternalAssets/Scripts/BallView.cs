using Mirror;
using UnityEngine;

public class BallView : NetworkBehaviour
{
    [SerializeField]
    private Rigidbody _rb;

    [SyncVar(hook = nameof(OnChangeOwnerID))]
    public int ownerID;

    private void OnChangeOwnerID(int last, int newValue) => 
        ownerID = newValue;

    [Server] 
    public void BallForce(Transform spawnPosition, float force, int owner)
    {
        if(!_rb)
            _rb = GetComponent<Rigidbody>();
        _rb.AddForce(spawnPosition.forward * (200 * (force + 1)));
        ownerID = owner;
    }
}