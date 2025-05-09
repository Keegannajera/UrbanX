using UnityEngine;
using UnityEditor.UI;

public class Interactable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject interactIconPrefab;
    public SphereCollider rangeCollider;
    public GameObject interactIcon;
    public float rangeRadius = 4;
    public float iconScalingMax = 1f;
    public float iconScalingMin = 0.5f;
    public GameObject player;
    public GameObject iconPanel;


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
        float distanceRatio = distance / rangeRadius;
        distanceRatio = Mathf.Clamp(distanceRatio, 0f, 1f);
        float scale = iconScalingMin + (distanceRatio * (iconScalingMax-iconScalingMin));
        iconPanel.transform.localScale = new Vector3(scale, scale, scale);


    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if(other.tag == "Player")
        {
            interactIcon.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        print(other.name);

        if (other.tag == "Player")
        {
            interactIcon.SetActive(false);
        }
    }

}
