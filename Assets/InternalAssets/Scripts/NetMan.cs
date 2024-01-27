using Mirror;
using TMPro;
public class NetMan : NetworkManager
{
    public TMP_InputField addressInput;

    public void StartHosting() => 
        singleton.StartHost();

    public void JoinGame()
    {
        singleton.networkAddress = addressInput.text;
        singleton.StartClient();
    }
}
