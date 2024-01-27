using TMPro;
using UnityEngine;

public class GatesManager : MonoBehaviour
{
    public static GatesManager Instance;
    
    public GameObject[] gates;

    public TextMeshProUGUI[] playerScoreIndexes;

    private void Awake() => 
        Instance ??= this;

    public void PlayersScoreUpdate()
    {
        for (var index = 0; index < gates.Length; index++)
        {
            if(!gates[index].gameObject.activeSelf) continue;
            var gate = gates[index].GetComponent<GatesView>();
            playerScoreIndexes[index].text = gate.counter.ToString();
        }
    }
}
