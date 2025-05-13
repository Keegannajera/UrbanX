using UnityEngine;

[RequireComponent (typeof(Collection))]
public class InteractableCollection : Interactable
{
    public Collection collection;


    protected override void Init()
    {
        base.Init();
        collection = GetComponent<Collection>();

    }



    protected override void Interact()
    {
        Debug.Log("Interacted with the item" + gameObject.name);
        //Perform Interaction here.
        collection.Play();
    }


}
