using UnityEngine;
using Mirror;
using Mirror.Examples.Basic;
using System.Linq;
using System.Collections.Generic;

public class PlayerManager : NetworkBehaviour
{
    // Is singleton
    public static PlayerManager instance;

    [SyncVar]
    public Roles[] playerRoles;

    [SyncVar]
    public List<int> players;
    public Player clientPlayer;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("More than one instance of PlayerManager found!");
            return;
        }
    }

    [Server]
    public void AddNewPlayer(int clientId)
    {
        players.Add(clientId);
    }

    // Server side
    [Server]
    public void GenerateRoles(int numPlayers)
    {
        // TODO: Generate roles based on the number of players
        // Function should be called everytime a number of players change / role settings change
        // For now, assume that only 2 players
        Roles[] generatedPlayerRoles = new Roles[2];
        generatedPlayerRoles[0] = Roles.Villager;
        generatedPlayerRoles[1] = Roles.WereRat;
        playerRoles = generatedPlayerRoles;
    }
}
