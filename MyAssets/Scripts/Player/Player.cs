using Mirror;
using Mirror.Examples.Basic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnRoleChanged))]
    public Roles role;

    public override void OnStartLocalPlayer()
    {
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0, 0, 0);
        PlayerManager.localPlayer = this;
        CmdAddNewPlayer(this);
    }

    [Server]
    public void SetRole(Roles newRole)
    {
        role = newRole;
    }

    [Command]
    public void CmdAddNewPlayer(Player player)
    {
        if (PlayerManager.instance == null)
        {
            return;
        }
        PlayerManager.instance.AddNewPlayer(player);
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

    public void OnRoleChanged(Roles oldRole, Roles newRole)
    {
        if (isLocalPlayer)
        {
            PlayerUIManager.instance.SetRoleText(newRole);
        }
    }
}
