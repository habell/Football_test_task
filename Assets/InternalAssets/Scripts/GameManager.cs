using UnityEngine;

namespace InternalAssets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public Color playerColor;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this) 
                Destroy(gameObject);
        }

        private void SetPlayerData(Color color) => 
            playerColor = color;

        public void SetColor(int id)
        {
            switch (id)
            {
                case 0:
                    SetPlayerData(Color.white);
                    break;
                case 1:
                    SetPlayerData(Color.red);
                    break;
                case 2:
                    SetPlayerData(Color.green);
                    break;
                case 3:
                    SetPlayerData(Color.blue);
                    break;
            }
        }
    }
}