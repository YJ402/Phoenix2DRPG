using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombItem : MonoBehaviour
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
            collider.GetComponent<RangeStatHandler>().BulletIndex=2;
            itemManager?.spawnedItems.Remove(this.gameObject);
            Debug.Log($"{this.gameObject} removed");
            Destroy(this.gameObject);
        }

    }
}
