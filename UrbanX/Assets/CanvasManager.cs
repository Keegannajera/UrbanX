using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEditor;
using TMPro;
using System.Collections.Generic;

public class CanvasManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private static CanvasManager instance = null;
    public static CanvasManager Instance {  get { return instance; } }

    public GameObject camCorderPanel;
    public GameObject logCollectionDisplayPanel;
    public GameObject videoCollectionDisplayPanel;

    public GameObject collectionInventoryPanel;
    public GameObject lastStayedPanel;

    public VideoPlayer videoPlayer;

    public GameObject collectionDisplayPrefab;
    public GameObject grid;
    public List<Collection> collections;

    public TMP_Text logPanelText;
    public bool afterInventoryClick = false;

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
        if(Input.GetKeyDown(KeyCode.I))
        {
            ShowCollectionDisplayPanel();
        }
    }

    public void DisplayLogCollection(string text)
    {
        camCorderPanel.SetActive(false);
        logCollectionDisplayPanel.SetActive(true);
        collectionInventoryPanel.SetActive(false);

        logPanelText.text = text;

        lastStayedPanel = logCollectionDisplayPanel;
        MouseScript.Instance.UnlockCursor();
    }
    public void EndLogCollection()
    {
        camCorderPanel.SetActive(true);
        logCollectionDisplayPanel.SetActive(false);

        if (lastStayedPanel == collectionInventoryPanel)
            collectionInventoryPanel.SetActive(true);
        lastStayedPanel = null;
        MouseScript.Instance.LockCursor();
    }

    public void DisplayVideoCollection()
    {
        camCorderPanel.SetActive(false);
        videoCollectionDisplayPanel.SetActive(true);
        collectionInventoryPanel.SetActive(false);


        videoPlayer.Play();

        lastStayedPanel = videoCollectionDisplayPanel;
        MouseScript.Instance.UnlockCursor();
    }

    public void EndVideoCollection()
    {
        camCorderPanel.SetActive(true);
        videoCollectionDisplayPanel.SetActive(false);
        videoPlayer.Pause();

        if (lastStayedPanel == collectionInventoryPanel)
            lastStayedPanel.SetActive(true);
        lastStayedPanel = null;
        MouseScript.Instance.LockCursor();
    }

    public void ShowCollectionDisplayPanel()
    {
        camCorderPanel.SetActive(false);
        logCollectionDisplayPanel.SetActive(false);
        videoCollectionDisplayPanel.SetActive(false);

        collectionInventoryPanel.SetActive(true);

        lastStayedPanel = collectionInventoryPanel;
        MouseScript.Instance.UnlockCursor();
    }

    public void CloseCollectionDisplayPanel()
    {
        camCorderPanel.SetActive(true);
        collectionInventoryPanel.SetActive(false);
        if(lastStayedPanel != null) 
            lastStayedPanel.SetActive(true);

        MouseScript.Instance.LockCursor();
    }

    public void AddCollectionToInventoryDisplay(Collection collection)
    {
        GameObject go = Instantiate(collectionDisplayPrefab, grid.transform);
        CollectionDisplay cd = go.GetComponent<CollectionDisplay>();
        cd.collection = collection;
        collections.Add(collection);
    }
}
