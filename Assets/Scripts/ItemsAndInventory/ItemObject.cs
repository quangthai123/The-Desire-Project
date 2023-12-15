using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private ItemData itemData;
    [SerializeField] private Rigidbody2D rb;



    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item-object "+ itemData.name;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!Inventory.instance.CanAddItem()&& itemData.itemType == ItemType.Equipment)
        {
            rb.velocity=new Vector2(0,7);
            return;
        }

        if (collision.GetComponent<Player>() != null)
        {
            Inventory.instance.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}
