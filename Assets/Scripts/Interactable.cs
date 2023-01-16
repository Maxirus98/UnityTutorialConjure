using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Item item;
    public bool canBePicked;
    public bool hasInteracted;

    public virtual void DoSomething()
    {
        throw new NotImplementedException();
    }
}
