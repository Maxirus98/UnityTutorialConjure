using System.Collections;
using UnityEngine;

public class Minigame : Interactable
{
    public CanvasGroup statsUi;
    public Camera camera;
    public GameObject miniGameUi;
    public PlayerInteraction playerInteraction;
    public DialogHandler dialogHandler;
    public Inventory inventory;
    public AudioSource wonSound;
    public AudioSource wrongSound;
    
    private Camera mainCamera;
    private readonly string RIGHT_ANSWER = "TLB";
    private string stringBuilder = "";
    private bool hasWon = false;
    
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ToggleMiniGame(false);
        }
        
        if (!hasWon && stringBuilder == RIGHT_ANSWER)
        {
            hasWon = true;
            stringBuilder = "";
            if (!wonSound.isPlaying)
            {
                wonSound.Play();
            }
            
            ToggleMiniGame(false);
            transform.parent.GetChild(0).gameObject.SetActive(true);
            Destroy(gameObject,2f);
        }
        else
        {
            if (stringBuilder.Length >= 3)
            {
                if (!wrongSound.isPlaying)
                {
                    wrongSound.Play();
                    StartCoroutine(dialogHandler.ShowAlert("Try again."));
                    stringBuilder = "";
                }
            }
        }
        
        
        
        if (!hasWon && hasInteracted)
        {
            StartCoroutine(HandleChoice());
        }
    }

    public override void DoSomething()
    {
        if (inventory.HasItem(item.itemName))
        {
            hasInteracted = true;
            if (hasWon)
            {
                return;
            }
        
            StartCoroutine(dialogHandler.ShowAlert("Was this bedroom really set like that? Choose the correct furnitures' order by pressing on the correct number", 0f));
            ToggleMiniGame(true);
        }
    }
    
    private IEnumerator HandleChoice()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            stringBuilder += "L";
            yield break;
        }
        
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            stringBuilder += "B";
            yield break;
        }
        
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            stringBuilder += "T";
        }

        yield return new WaitForSeconds(1f);
    }

    private void ToggleMiniGame(bool isActive)
    {
        statsUi.alpha = isActive ? 0 : 1;
        mainCamera.gameObject.SetActive(!isActive);
        playerInteraction.HideText();
        camera.gameObject.SetActive(isActive);
        miniGameUi.SetActive(isActive);
        stringBuilder = "";
    }
}
