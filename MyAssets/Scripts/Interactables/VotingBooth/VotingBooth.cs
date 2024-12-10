using UnityEngine;
using Mirror;

public class VotingBooth : Interactable
{

    [SerializeField] private GameObject votingSlipCanvas;

    [Client]
    public override void Interact()
    {
        votingSlipCanvas.SetActive(true);
    }
}
