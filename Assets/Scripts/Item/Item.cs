using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    private ResourceController resourceController;
    public ItemManager itemManager;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            resourceController.ChangeHealth(20f);
        }
        itemManager.spawnedItems.Remove(this.gameObject);
        Debug.Log($"{this.gameObject} removed");
        Destroy(this);
    }
}