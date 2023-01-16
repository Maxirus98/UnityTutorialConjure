using UnityEngine;

public class Pills : Interactable
{
    public DialogHandler dialogHandler;
    
    private PlayerStats playerStats;
    void Start()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        dialogHandler = playerStats.gameObject.GetComponent<DialogHandler>();
    }
    public override void DoSomething()
    {
        playerStats.mental = 100f;
        StartCoroutine(dialogHandler.ShowAlert("You take a happy pill and gain your mental health back"));
    }
}
