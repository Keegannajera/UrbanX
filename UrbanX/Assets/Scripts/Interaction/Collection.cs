using UnityEngine;

public enum CollectionType
{
    Video,
    Audio,
    Log,
}

public class Collection: MonoBehaviour
{

    //public CollectionType type;

    //public string name;

    //public string text;
    //public Object video;
    //public Object audio;

    public Item item;

    public void Play()
    {
        Debug.Log("Enter Collection Play");
        if(item.type== CollectionType.Log)
        {
            CanvasManager.Instance.DisplayLogCollection(item.text);
        }
        else if(item.type ==CollectionType.Video)
        {
            CanvasManager.Instance.DisplayVideoCollection(item.video);
        }
        else if (item.type == CollectionType.Audio)
        {
            CanvasManager.Instance.DisplayAudioCollection(item.audio);
        }
    }

}
