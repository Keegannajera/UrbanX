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
        if (collection.item.isRemoveAfterInteract)
        {
            CanvasManager.Instance.AddCollectionToInventoryDisplay(collection);
            Destroy(gameObject);
        }
        else if (!interactedOnce)
        {
            CanvasManager.Instance.AddCollectionToInventoryDisplay(collection);
            interactedOnce = true;

        }

    }


}
