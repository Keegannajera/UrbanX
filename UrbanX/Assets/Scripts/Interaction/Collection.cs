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

    public string text;
    public Object video;
    public Object audio;

    public void Play()
    {
        Debug.Log("Enter Collection Play");
        if(type== CollectionType.Log)
        {
            CanvasManager.Instance.DisplayLogCollection(text);
        }
        else if(type==CollectionType.Video)
        {
            CanvasManager.Instance.DisplayVideoCollection();
        }
    }

}
