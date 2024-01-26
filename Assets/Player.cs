using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField]
    private BallView ball;
    [SerializeField]
    private Transform ballForwardSpawnPosition;

    private void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetMouseButtonUp(1)) 
            CreateBall();
        
        float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        float speed = 5f * Time.deltaTime;
        transform.Translate(new Vector2(h * speed, transform.position.y));
    }
    
    [Command]
    private void CreateBall()
    {
        BallView _ball = Instantiate(ball);
        NetworkServer.Spawn(_ball.gameObject);
        _ball.transform.position = ballForwardSpawnPosition.position;
        _ball.BallForce(ballForwardSpawnPosition);
        print(_ball.transform.position);
        Destroy(_ball.gameObject, 10); 
    }
}