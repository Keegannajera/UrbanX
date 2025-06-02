using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    public CollectionType type;

    public string name;

    [TextArea]
    public string text;
    public Object video;
    public Object audio;
    public bool isRemoveAfterInteract = false;
}
