using UnityEngine;

public enum CollectionType
{
    Video,
    Audio,
    Log,
}

public class Collection: MonoBehaviour
{

    public CollectionType type;


    public void Play()
    {
        Debug.Log("Enter Collection Play");
        if(type== CollectionType.Log)
        {
           CanvasManager.Instance.DisplayLogCollection();
        }
    }

}
