using UnityEngine;
using UnityEditor.UI;

//public interface IInteractable
//{
//    void Interact();
//}


public class Interactable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject interactIconPrefab;
    public SphereCollider rangeCollider;
    public GameObject interactIcon;
    public float rangeRadius = 5f;
    public float rangeRadiusMin = 2f;

    public float iconScalingMax = 1f;
    public float iconScalingMin = 0.5f;
    public GameObject player;
    public GameObject iconPanel;

    public bool interactable = false;
    public bool isremoveAfterInteract = true;



    void Start()
    {
        Debug.Log(interactIconPrefab.name);
        interactIconPrefab = (GameObject)Resources.Load("CanvasInteractable");
        interactIcon = Instantiate(interactIconPrefab, transform);
        interactIcon.SetActive(false);
        rangeCollider = GetComponent<SphereCollider>();
        rangeCollider.radius = rangeRadius;
        player = GameObject.FindGameObjectWithTag("Player");
        iconPanel = interactIcon.transform.Find("Panel").gameObject;

        Init();
    }

    protected virtual void Init() { }

    // Update is called once per frame
    void Update()
    {
        float distance = (player.transform.position - transform.position).magnitude;

        float distanceRatio = distance > rangeRadiusMin ? (distance - rangeRadiusMin) / (rangeRadius - rangeRadiusMin) : 0;
        distanceRatio = Mathf.Clamp(distanceRatio, 0f, 1f);
        float scale = iconScalingMin + ((1 - distanceRatio) * (iconScalingMax-iconScalingMin));
        iconPanel.transform.localScale = new Vector3(scale, scale, scale);

        iconPanel.transform.LookAt(player.transform.position);
        iconPanel.transform.rotation = Quaternion.Euler(0, iconPanel.transform.rotation.eulerAngles.y - 180, 0);
        //iconPanel.transform.Rotate(iconPanel.transform.up, 180);


        if (Input.GetKeyDown(KeyCode.E) && interactable)
        {
            //gameObject.GetComponent<IInteractable>().Interact();
            Interact();
        }

    }

    protected virtual void Interact() { }
    //{
    //    Debug.Log("Interacted with the item" + gameObject.name);
    //    //Perform Interaction here.
    //}

    private void OnTriggerEnter(Collider other)
    {
        //print(other.name);
        //other.gameObject.GetComponent<IInteractable>();
        if(other.tag == "Player")
        {
            if (interactIcon != null)
                interactIcon.SetActive(true);
            interactable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //print(other.name);

        if (other.tag == "Player")
        {
            if (interactIcon != null)
                interactIcon.SetActive(false);
            interactable = false;
        }
    }

}
