using System;
using Mirror;
using TMPro;
using UnityEngine;

namespace InternalAssets.Scripts
{
    public class PlayerScoreColorChanger : NetworkBehaviour
    {
        [SyncVar(hook = nameof(OnColorChanged))]
        public Color color;

        private void OnColorChanged(Color oldColor, Color newColor)
        {
            newColor.a = 1f;
            GetComponent<TextMeshProUGUI>().color = newColor;   
        }
    }
}