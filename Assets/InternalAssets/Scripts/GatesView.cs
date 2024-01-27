using System;
using Mirror;
using UnityEngine;

public class GatesView : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnCountChanged))]
    public int counter;

    [SyncVar(hook = nameof(OnColorChanged))]
    public Color color;

    [SyncVar(hook = nameof(OnAnimationChanged))]
    private bool _animated;

    [SerializeField]
    private bool inverted;

    private bool _left = true;

    private const float MaxYPos = 0.63f;

    private float _additionalZPos = 0;

    private void OnColorChanged(Color oldColor, Color newColor)
    {
        GetComponent<Renderer>().material.color = newColor;
        _animated = true;
    }

    private void OnCountChanged(int oldValue, int newValue)
    {
        counter = newValue;
        GatesManager.Instance.PlayersScoreUpdate();
    }

    private void OnAnimationChanged(bool oldValue, bool newValue) =>
        _animated = newValue;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<BallView>() || !isServer) return;

        Destroy(other.gameObject);
        counter--;
        GatesManager.Instance.gates[other.GetComponent<BallView>().ownerID].GetComponent<GatesView>().counter++;
    }

    private void Update()
    {
        if (!_animated) return;

        var deltaTime = Time.deltaTime;
        if (transform.position.y < MaxYPos)
            transform.position += new Vector3(0, deltaTime / 3, 0);
        else
        {
            var value = _left ? deltaTime : -deltaTime;
            _additionalZPos += value;
            if (_left)
            {
                if (_additionalZPos >= 3)
                    _left = false;
            }
            else
            {
                if (_additionalZPos <= -3)
                    _left = true;
            }

            transform.position += new Vector3(inverted ? value : 0, 0, inverted ? 0 : value);
        }
    }
}