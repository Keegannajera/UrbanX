using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEditor;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private static CanvasManager instance = null;
    public static CanvasManager Instance {  get { return instance; } }

    public GameObject camCorderPanel;
    public GameObject logCollectionDisplayPanel;
    public GameObject audioCollectionDisplayPanel;
    public GameObject videoCollectionDisplayPanel;

    public GameObject collectionInventoryPanel;
    public GameObject lastStayedPanel;

    public VideoPlayer videoPlayer;
    public AudioSource videoAudioSource;
    public AudioSource audioSource;

    public GameObject collectionDisplayPrefab;
    public GameObject grid;
    public List<Collection> collections;

    public TMP_Text logPanelText;
    public bool afterInventoryClick = false;
    public Slider slider;

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

        if (audioSource.isPlaying)
        {
            slider.value = audioSource.time / audioSource.clip.length;
        }
    }
    
    #region Log
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
        logCollectionDisplayPanel.SetActive(false);

        if (lastStayedPanel == collectionInventoryPanel || afterInventoryClick)
        {
            collectionInventoryPanel.SetActive(true);
            lastStayedPanel = null;

            return;
        }

        camCorderPanel.SetActive(true);
        lastStayedPanel = null;
        MouseScript.Instance.LockCursor();
    }
    #endregion


    #region Audio
    public void DisplayAudioCollection(Object clip)
    {
        camCorderPanel.SetActive(false);
        audioCollectionDisplayPanel.SetActive(true);
        collectionInventoryPanel.SetActive(false);

        audioSource.clip = (AudioClip)clip;
        audioSource.Stop();
        audioSource.Play();

        lastStayedPanel = logCollectionDisplayPanel;
        MouseScript.Instance.UnlockCursor();
    }
    public void EndAudioCollection()
    {
        audioSource.Pause();
        audioCollectionDisplayPanel.SetActive(false);

        if (lastStayedPanel == collectionInventoryPanel || afterInventoryClick)
        {
            collectionInventoryPanel.SetActive(true);
            lastStayedPanel = null;

            return;
        }

        camCorderPanel.SetActive(true);
        lastStayedPanel = null;
        MouseScript.Instance.LockCursor();
    }
    #endregion

    #region Video

    public void DisplayVideoCollection(Object clip)
    {
        camCorderPanel.SetActive(false);
        videoCollectionDisplayPanel.SetActive(true);
        collectionInventoryPanel.SetActive(false);

        videoPlayer.clip = (VideoClip)clip;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, videoAudioSource);

        videoPlayer.Stop();
        videoPlayer.Play();

        lastStayedPanel = videoCollectionDisplayPanel;
        MouseScript.Instance.UnlockCursor();
    }

    public void EndVideoCollection()
    {
        videoPlayer.Pause();
        videoCollectionDisplayPanel.SetActive(false);

        if (lastStayedPanel == collectionInventoryPanel || afterInventoryClick)
        {
            collectionInventoryPanel.SetActive(true);
            lastStayedPanel = null;

            return;
        }

        camCorderPanel.SetActive(true);
        lastStayedPanel = null;
        MouseScript.Instance.LockCursor();
    }
    #endregion


    #region Collection Inventory
    public void ShowCollectionDisplayPanel()
    {
        camCorderPanel.SetActive(false);
        logCollectionDisplayPanel.SetActive(false);
        videoCollectionDisplayPanel.SetActive(false);

        collectionInventoryPanel.SetActive(true);

        lastStayedPanel = collectionInventoryPanel;
        afterInventoryClick = true;
        MouseScript.Instance.UnlockCursor();
    }

    public void CloseCollectionDisplayPanel()
    {
        camCorderPanel.SetActive(true);
        collectionInventoryPanel.SetActive(false);


        afterInventoryClick = false;
        if(lastStayedPanel && lastStayedPanel != collectionInventoryPanel)
        {
            lastStayedPanel.SetActive(true);
            return; 
        }

        MouseScript.Instance.LockCursor();
    }

    public void AddCollectionToInventoryDisplay(Collection collection)
    {
        GameObject go = Instantiate(collectionDisplayPrefab, grid.transform);
        CollectionDisplay cd = go.GetComponent<CollectionDisplay>();
        cd.collection = collection;
        go.transform.Find("Name").GetComponent<TMP_Text>().text = collection.item.name;
        collections.Add(collection);
    }

    #endregion
}
