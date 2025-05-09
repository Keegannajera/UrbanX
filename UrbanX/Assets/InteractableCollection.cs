using UnityEngine;

public class InteractableCollection : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interacted with the item" + gameObject.name);
        //Perform Interaction here.
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
