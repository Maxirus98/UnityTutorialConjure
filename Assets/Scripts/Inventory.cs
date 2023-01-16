using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    private readonly string MISSING_ITEM = "You're missing something";
    private DialogHandler dialogHandler;

    void Start()
    {
        dialogHandler = GetComponent<DialogHandler>();
    }

    public void PickUpItem(Item item)
    {
       if (items.IndexOf(item) < 0)
       {
           items.Add(item);
           StartCoroutine(dialogHandler.ShowAlert($"You pick up: {item.itemName}"));
       }
    }

    public bool HasItem(string itemName)
    {
        return items.Find(i => i.itemName.Equals(itemName));
    }

    public void UseItem(Item item)
    {
        if (items.IndexOf(item) < 0)
        {
            StartCoroutine(dialogHandler.ShowAlert(MISSING_ITEM));
            return;
        }
        
        if (item.oneTimeUsage)
        {
            items.Remove(item);
        }
    }
}
