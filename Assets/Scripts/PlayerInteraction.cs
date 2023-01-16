using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public CanvasGroup interactUi;
    public DialogHandler dialogHandler;
    public Interactable focus;

    private Inventory inventory;
    private bool disabledUi;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        dialogHandler = GetComponent<DialogHandler>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && focus && !focus.hasInteracted)
        {
            Interact();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            if (focus == null)
            {
                focus = other.GetComponent<Interactable>();
                interactUi.gameObject.SetActive(true);
            }
        }

        if (other.CompareTag("Bathroom"))
        {
            StartCoroutine(dialogHandler.ShowAlert("It seems to be showering in the dark...", 5f));
        }
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            if (focus && focus.hasInteracted)
            {
                interactUi.gameObject.SetActive(false);
            }
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable") && focus)
        {
            interactUi.gameObject.SetActive(false);
            focus.hasInteracted = false;
            focus = null;
        }
    }
    
    public void HideText()
    {
        interactUi.gameObject.SetActive(false);
    }

    private void Interact()
    {
        if (!focus) return;
        
        
        if (focus.canBePicked)
        {
            inventory.PickUpItem(focus.item);
            HideText();
            focus.transform.parent.gameObject.SetActive(false);
            focus = null;
        }
        else
        {
            focus.DoSomething();
            if (focus.item)
            {
                inventory.UseItem(focus.item);
            }
        }
    }
}
