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
        switch (collection.type)
        {
            case CollectionType.Log:
                btn.onClick.AddListener(CanvasManager.Instance.DisplayLogCollection);
                break;
            case CollectionType.Video:
                btn.onClick.AddListener(CanvasManager.Instance.DisplayVideoCollection);
                break;

        } 
    }

    private void OnDisable()
    {
        btn.onClick.RemoveAllListeners();
    }
}
