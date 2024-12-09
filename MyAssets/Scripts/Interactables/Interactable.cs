using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{

    public void Highlight()
    {
        Debug.Log("Highlighting " + gameObject.name);
        gameObject.GetComponent<Outline>().enabled = true;
    }

    public void Unhighlight()
    {
        Debug.Log("Unhighlighting " + gameObject.name);
        gameObject.GetComponent<Outline>().enabled = false;
    }

    public void Interact()
    {
        Debug.Log("Interacting with " + gameObject.name);
    }
}
