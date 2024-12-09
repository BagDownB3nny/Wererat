using UnityEngine;
using Mirror;

public class VotingBooth : Interactable
{

    [Client]
    public override void Interact()
    {
        Debug.Log("Interacting with VotingBooth");
    }
}
