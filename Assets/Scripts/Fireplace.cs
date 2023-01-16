using UnityEngine;

public class Fireplace : Interactable
{
    private ParticleSystem particle;
    private PlayerStats playerStats;
    private Inventory inventory;
    public DialogHandler dialogHandler;

    void Start()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        inventory = playerStats.gameObject.GetComponent<Inventory>();
        dialogHandler = playerStats.gameObject.GetComponent<DialogHandler>();
        particle = GetComponent<ParticleSystem>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && particle.isPlaying)
        {
            playerStats.IsHeatingUp = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && particle.isPlaying)
        {
            playerStats.IsHeatingUp = false;
            dialogHandler.HideAlert();
        }
    }

    public override void DoSomething()
    {
        if (inventory.HasItem("Candle"))
        {
            if (particle && !particle.isPlaying)
            {
                particle.Play();
                hasInteracted = true;
            }
        }
    }
}
