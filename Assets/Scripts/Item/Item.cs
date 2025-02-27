using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemManager itemManager;
    private void Awake()
    {
        itemManager = GetComponentInParent<ItemManager>();
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collider.GetComponent<ResourceController>().ChangeHealth(100);
            itemManager?.spawnedItems.Remove(this.gameObject);
            Debug.Log($"{this.gameObject} removed");
            Destroy(this.gameObject);
        }
        
    }
}