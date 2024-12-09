using Mirror;
using Mirror.Examples.Basic;
using Steamworks;
using TMPro;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnRoleChanged))]
    public Roles role;
    public string steamUsername;
    [SerializeField] private TMP_Text playerUIPrefab;

    public override void OnStartLocalPlayer()
    {
        Camera.main.transform.GetComponent<MoveCamera>().SetCameraPosition(this.transform);
        Camera.main.transform.GetComponent<PlayerCamera>().SetOrientation(this.transform);
        Camera.main.transform.localPosition = new Vector3(0, 0, 0);
        PlayerManager.localPlayer = this;
        Debug.Log("Local player set");

        if (SteamManager.Initialized)
        {
            steamUsername = SteamFriends.GetPersonaName();
        }
        else
        {
            steamUsername = "Player " + Random.Range(0, 1000);
        }
        playerUIPrefab.text = steamUsername;
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

    public void OnRoleChanged(Roles oldRole, Roles newRole)
    {
        if (isLocalPlayer)
        {
            PlayerUIManager.instance.SetRoleText(newRole);
        }
    }
}
