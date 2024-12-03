using Mirror;
using UnityEngine;

public class PlayerScript : NetworkBehaviour
{

    private int clientId;

    public override void OnStartLocalPlayer()
    {
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0, 0, 0);
        clientId = NetworkClient.connection.connectionId;
        // CmdAddNewPlayer(clientId);
    }

    [Command]
    public void CmdAddNewPlayer(int id)
    {
        PlayerManager.instance.AddNewPlayer(id);
    }

    void Update()
    {
        if (!isLocalPlayer) { return; }

        HandleMovement();
    }

    [Client]
    public void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 110.0f;
        float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * 4f;

        transform.Rotate(0, moveX, 0);
        transform.Translate(0, 0, moveZ);
    }
}
