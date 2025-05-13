using Unity.VisualScripting;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private static CanvasManager instance = null;
    public static CanvasManager Instance {  get { return instance; } }

    public GameObject camCorderPanel;
    public GameObject logCollectionDisplayPanel;


    void Awake()
    {
        if(instance != null&& instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        logCollectionDisplayPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayLogCollection()
    {
        camCorderPanel.SetActive(false);
        logCollectionDisplayPanel.SetActive(true);
        MouseScript.Instance.UnlockCursor();
    }
    public void EndLogCollection()
    {
        camCorderPanel.SetActive(true);
        logCollectionDisplayPanel.SetActive(false);
        MouseScript.Instance.LockCursor();

    }



}
