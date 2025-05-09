using UnityEngine;
using UnityEditor.UI;

public interface IInteractable
{
    void Interact();
}


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

    void Start()
    {
        interactIconPrefab = (GameObject)Resources.Load("CanvasInteractable");
        interactIcon = Instantiate(interactIconPrefab, transform);
        interactIcon.SetActive(false);
        rangeCollider = GetComponent<SphereCollider>();
        rangeCollider.radius = rangeRadius;
        player = GameObject.Find("Player");
        iconPanel = interactIcon.transform.Find("Panel").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (player.transform.position - transform.position).magnitude;

        float distanceRatio = distance > rangeRadiusMin ? (distance - rangeRadiusMin) / (rangeRadius - rangeRadiusMin) : 0;
        distanceRatio = Mathf.Clamp(distanceRatio, 0f, 1f);
        float scale = iconScalingMin + ((1 - distanceRatio) * (iconScalingMax-iconScalingMin));
        iconPanel.transform.localScale = new Vector3(scale, scale, scale);

        //print(distance +", "+ distanceRatio + ", "+ scale);

        if (Input.GetKeyDown(KeyCode.E) && interactable)
        {
            gameObject.GetComponent<IInteractable>().Interact();
            //Interact();
        }

    }

    //public void Interact()
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
            interactIcon.SetActive(true);
            interactable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //print(other.name);

        if (other.tag == "Player")
        {
            interactIcon.SetActive(false);
            interactable = false;
        }
    }

}
