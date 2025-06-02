using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


public class CollectionDisplay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Button btn;
    public Collection collection;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        switch (collection.item.type)
        {
            case CollectionType.Log:
                btn.onClick.AddListener(()=> {CanvasManager.Instance.DisplayLogCollection(collection.item.text); });
                break;
            case CollectionType.Video:
                btn.onClick.AddListener(() => { CanvasManager.Instance.DisplayVideoCollection(collection.item.video); });
                break;
            case CollectionType.Audio:
                btn.onClick.AddListener(() => { CanvasManager.Instance.DisplayVideoCollection(collection.item.audio); });
                break;

        } 
    }

    private void OnDisable()
    {
        btn.onClick.RemoveAllListeners();
    }
}
