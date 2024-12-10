using System;
using System.Collections.Generic;
using Mirror;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class VotingSlip : MonoBehaviour
{
    [SerializeField] private GameObject VotingButtonsContainer;
    private List<String> playerNames;
    private List<String> deadPlayerNames;
    [SerializeField] private GameObject votingPlayerRow;

    public void ExitVotingSlip()
    {
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        if (playerNames == null)
        {
            GenerateVotingSlip();
        }
    }

    private void GenerateVotingSlip()
    {
        playerNames = GetPlayerNames();
        for (int i = 0; i < playerNames.Count; i++)
        {
            GameObject votingRow = Instantiate(votingPlayerRow, new Vector3(0, 0, 0), Quaternion.identity);
            votingRow.transform.SetParent(VotingButtonsContainer.transform);
            votingRow.GetComponentInChildren<TMP_Text>().text = playerNames[i];
        }
    }

    private List<String> GetPlayerNames()
    {
        List<String> playerNames = new List<String>();
        PlayerManager.instance.GetPlayerList().ForEach(player => playerNames.Add(player.steamUsername));
        return playerNames;
    }
}
