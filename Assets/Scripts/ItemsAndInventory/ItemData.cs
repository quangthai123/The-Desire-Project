using System.Text;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
public enum ItemType
{
    Quest,
    Equipment
}

[CreateAssetMenu(fileName = "New item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string name;
    public Sprite icon;
    public string itemId;

    protected StringBuilder sb = new StringBuilder();

    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = UnityEditor.AssetDatabase.GetAssetPath(this);
        itemId = UnityEditor.AssetDatabase.AssetPathToGUID(path);
#endif
    }

    public virtual string GetDescription()
    {
        return "";
    }

    // Use this method to load the asset
    public static ItemData LoadItemData(string itemName)
    {
        // The path to the asset is relative to the "Resources" folder
        string path = "ItemData/" + itemName; // Adjust the path accordingly

        // Load the asset using Resources.Load
        ItemData loadedItem = Resources.Load<ItemData>(path);

        return loadedItem;
    }
}
