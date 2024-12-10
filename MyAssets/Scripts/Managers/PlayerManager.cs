using UnityEngine;
using Mirror;
using Mirror.Examples.Basic;
using System.Linq;
using System.Collections.Generic;

public class PlayerManager : NetworkBehaviour
{
    // Is singleton
    public static PlayerManager instance;

    // A list containing all the roles for the current lobby
    // This list should be updated everytime a player joins or leaves the lobby
    // For example, in a 6 player game, there could be 1 werewolf, 1 seer, 1 medium, and 3 villagers
    public Roles[] playerRoles;

    // A list of all the players in the current lobby
    // Only server needs this list to assign roles
    public List<Player> players = new List<Player>();

    public static Player localPlayer;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("More than one instance of PlayerManager found!");
            return;
        }
    }

    public void HandleNetworkStop()
    {
        // Destroy(gameObject);
    }

    [Server]
    public void AddNewPlayer(Player player)
    {
        players.Add(player);
        if (NetworkServer.connections.Count == players.Count)
        {
            OnAllPlayersLoaded();
        }
    }

    [Server]
    public void OnAllPlayersLoaded()
    {
        AssignRoles();
    }

    public List<Player> GetPlayerList()
    {
        return players;
    }

    [Server]
    public void AssignRoles()
    {
        playerRoles = GenerateRoles();
        // Shuffle the roles
        playerRoles = playerRoles.OrderBy(x => Random.value).ToArray();

        // Assign roles to players
        for (int i = 0; i < players.Count; i++)
        {
            players[i].SetRole(playerRoles[i]);
        }
    }

    [Server]
    public Roles[] GenerateRoles()
    {
        // This is just a placeholder
        // In a real game, you would have a more complex algorithm to generate roles
        // For example, in a 6 player game, there could be 1 werewolf, 1 seer, 1 medium, and 3 villagers
        Roles[] roles = new Roles[players.Count];
        for (int i = 0; i < roles.Length; i++)
        {
            if (i == 0)
            {
                roles[i] = Roles.WereRat;
            }
            else if (i == 1)
            {
                roles[i] = Roles.Seer;
            }
            else if (i == 2)
            {
                roles[i] = Roles.Medium;
            }
            else
            {
                roles[i] = Roles.Villager;
            }
        }
        return roles;
    }
}
