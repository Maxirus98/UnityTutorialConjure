using UnityEngine;

public class MonsterLamp : Interactable
{
    public DialogHandler dialogHandler;
    public GameObject showeringGhost;
    public AudioSource wonSound;
    
    private Inventory inventory;
    void Start()
    {
        inventory =  GameObject.FindWithTag("Player").GetComponent<Inventory>();
        dialogHandler = inventory.GetComponent<DialogHandler>();
    }

    public override void DoSomething()
    {
        if(inventory.HasItem(item.itemName))
        {
            transform.parent.GetChild(0).gameObject.SetActive(true);
            transform.parent.GetChild(1).gameObject.SetActive(false);
            transform.parent.GetChild(2).gameObject.SetActive(true);
            if (!wonSound.isPlaying)
            {
                wonSound.Play();
            }
            StartCoroutine(dialogHandler.ShowAlert("The room lights up, the ghost seems to have disappeared... It left something.", 0f));
            showeringGhost.SetActive(false);
            hasInteracted = true;
        }
    }
}
