using UnityEngine;

public class EventTriggerType : MonoBehaviour
{
    public enum EventType
    {
        // Floor 1 event triggers
        TYPE_A = 0, TYPE_B = 1, TYPE_C = 2, TYPE_D = 3,
        // Floor 2 event triggers
        TYPE_E = 4, TYPE_F = 5, TYPE_G = 6
    }

    [SerializeField] private EventType eventType = EventType.TYPE_A;
    [SerializeField] private GameObject _missingPersonCanvas;

    // Conditions shared between event trigger objects 
    private static int _numInteractedEventsC = 0;
    private static int _numEventsC = 0;
    private static bool _eventASatisfied = false;
    private static bool _eventBSatisfied = false;
    private static bool _eventsCSatisfied = false;

    // Unique to each event trigger object
    private bool _eventCInteracted = false;  // Must be changed to true ONLY ONCE
    private bool _isReading = false;  // Toggle
    private bool _insideEventCArea = false;

    // Mainly for use with event trigger C
    void Start()
    {
        if (this.eventType == EventType.TYPE_C)
            _numEventsC++;
    }

    // Mainly for use with event trigger C
    void Update()
    {
        if (this.eventType == EventType.TYPE_C && _insideEventCArea)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Interacted with missing person poster");
                ReadMissingPersonPoster();

                if (!_eventCInteracted)
                    _numInteractedEventsC++;

                _eventCInteracted = true;
            }
        }
    }

    void OnTriggerEnter()
    {
        ActivateEventAction();
    }

    // Mainly used for event trigger C; event C condition is satisfied when all posters have been interacted with at least once
    void OnTriggerStay(Collider other)
    {
        if (this.eventType == EventType.TYPE_C)
        {
            if (_numInteractedEventsC == _numEventsC)
            {
                _eventsCSatisfied = true;
                Debug.Log("Triggers C have been satisfied!");
            }
        }
    }

    // Mainly used for event trigger C
    void OnTriggerExit()
    {
        if (this.eventType == EventType.TYPE_C)
        {
            Debug.Log("Exited event trigger C area");
            _insideEventCArea = false;
            _missingPersonCanvas.SetActive(false);
        }
    }

    private void ActivateEventAction()
    {
        switch (eventType)
        {
            case EventType.TYPE_A:
                Debug.Log("Auto save activated");
                _eventASatisfied = true;
                break;

            case EventType.TYPE_B:
                Debug.Log("Close front door, activate save");
                CloseFrontDoor();
                _eventBSatisfied = true;
                break;

            case EventType.TYPE_C:
                Debug.Log("Entered event trigger C area");
                _insideEventCArea = true;
                break;

            case EventType.TYPE_D:
                if (_eventASatisfied && _eventBSatisfied && _eventsCSatisfied)
                {
                    Debug.Log("Stairs opened");
                    OpenStaircaseDoors();
                }
                break;

            case EventType.TYPE_E:
                Debug.Log("Keegan gone missing video plays");
                break;

            case EventType.TYPE_F:
                Debug.Log("Audio of a woman's 911 call plays");
                break;

            case EventType.TYPE_G:
                Debug.Log("Auto save activated, 2nd floor balcony doors opened");
                break;
        }

        // Event trigger C will be the only event trigger that will persist; trigger D will be deleted when
        // the three conditions have been satisfied and the player enters it
        if (this.eventType != EventType.TYPE_C)
        {
            if (this.eventType == EventType.TYPE_D && (_eventASatisfied && _eventBSatisfied && _eventsCSatisfied))
                Destroy(this.gameObject);  // Destroy trigger D if the three conditions are satisfied 
            else if (this.eventType != EventType.TYPE_D)
                Destroy(this.gameObject);  // Destroy all other event triggers except for events C & D (conditional)
        }
    }

    // Event B method
    private void CloseFrontDoor()
    {
        GameObject.Find("DoorLeft").transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
    }

    // Event D method
    private void OpenStaircaseDoors()
    {
        GameObject.Find("DoorRight (13)").transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f) );
        GameObject.Find("DoorRight (14)").transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f) );
    }

    // Event C method
    private void ReadMissingPersonPoster()
    {
        _isReading = !_isReading;
        _missingPersonCanvas.SetActive(_isReading);
    }
}
