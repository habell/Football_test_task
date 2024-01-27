using System;
using System.Globalization;
using InternalAssets.Scripts;
using Mirror;
using TMPro;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnColorChanged))]
    public Color playerColor;

    [SyncVar(hook = nameof(OnPlayerIndexChanged))]
    private int _playerIndex;

    [SerializeField]
    private BallView ball;

    [SerializeField]
    private Transform ballForwardSpawnPosition;

    [SerializeField]
    private TextMeshProUGUI forceText;

    [SerializeField]
    private Renderer[] renderers;

    [SerializeField]
    private GameObject[] gatesRenderers;

    private float _force;
    
    private GatesManager _gatesManager;

    private void OnColorChanged(Color oldColor, Color newColor)
    {
        renderers[0].material.color = newColor;
        renderers[1].material.color = newColor;
    }

    private void OnPlayerIndexChanged(int old, int newValue) =>
        _playerIndex = newValue;

    [ClientRpc]
    private void RpcSetGateColor(int index)
    {
        if (index < 0) index = 3; // last player have index 0 sorry za kostili
        _gatesManager ??= GatesManager.Instance;
        gatesRenderers = _gatesManager.gates;

        _gatesManager.playerScoreIndexes[index].GetComponent<PlayerScoreColorChanger>().color = playerColor;

        gatesRenderers[index].GetComponent<GatesView>().color = playerColor;
    }

    [Command]
    private void CmdEnableAndSetColorToGate() =>
        RpcSetGateColor(NetworkManager.startPositionIndex - 1);

    [Command]
    private void CmdSetPlayerColor(Color color) =>
        playerColor = color;

    [Command]
    private void CmdSetPlayerIndex()
    {
        var index = NetworkManager.startPositionIndex - 1;
        _playerIndex = index < 0 ? 3 : index;
    }

    [Command]
    private void CmdCreateBall(float force)
    {
        var position = ballForwardSpawnPosition.position;
        BallView _ball = Instantiate(ball, position, Quaternion.identity);
        _ball.transform.position = position;
        _ball.BallForce(ballForwardSpawnPosition, force, _playerIndex);
        print(_ball.transform.position);
        NetworkServer.Spawn(_ball.gameObject);
        Destroy(_ball.gameObject, 10);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        Color color = GameManager.Instance.playerColor;

        CmdSetPlayerColor(color);
        CmdEnableAndSetColorToGate();
        CmdSetPlayerIndex();
    }

    private void Awake() =>
        forceText = GameObject.FindWithTag("ForceText").GetComponent<TextMeshProUGUI>();

    private void Update()
    {
        if (!isLocalPlayer) return;
        if (forceText)
            forceText.text = _force.ToString("0.00");

        if (_force >= 10) _force = 10;
        if (Input.GetMouseButton(0) && _force < 10)
            _force += Time.deltaTime * 4;

        if (!Input.GetMouseButtonUp(0)) return;
        CmdCreateBall(_force);
        _force = 0;
    }
}