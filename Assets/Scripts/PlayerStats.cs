using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider freezingSlider;
    public Slider mentalSlider;
    public GameEnding gameEnding;
    public TextMeshProUGUI endMessageUi;
    public float freezingness = 0f;
    public float mental = 100f;
    
    [field:SerializeField] public bool IsHeatingUp { get; set; }
    
    private readonly float MAX_FREEZING = 100f;
    private readonly float MAX_MENTAL = 100f;
    
    private float freezingSliderVelocity = 0;
    private float mentalSliderVelocity = 0;

    private float freezingTimestamp;
    private float mentalTimestamp;
    private float mentalImmunityTime = 5f;
    private float freezingRate = 10f;
    private float freezingIncrement = 5f;
    private float mentalDecrement = 25f;

    private DialogHandler dialogHandler;
    
        
    void Start()
    {
        dialogHandler = GetComponent<DialogHandler>();
    }

    void Update()
    {
        Freeze();
        AnimateSliderValue();

        if (mental <= 0f)
        {
            endMessageUi.text = "You're going crazy.";
            endMessageUi.color = Color.red;
            gameEnding.HandleZeroMental();
        }

        if (freezingness >= MAX_FREEZING)
        {
            endMessageUi.text = "You freeze in place.";
            endMessageUi.color = Color.cyan;
            gameEnding.HandleZeroMental();
        }
    }
    
    public void LoseMental()
    {
        if (Time.time > mentalTimestamp)
        {
            mentalTimestamp = Time.time + mentalImmunityTime;
            mental -= mentalDecrement;
            audioSource.Play();
            if (mental <= MAX_MENTAL/2)
            {
                StartCoroutine(dialogHandler.ShowAlert("You're starting to lose it."));
                return;
            }
            
            StartCoroutine(dialogHandler.ShowAlert("A creature scares you."));
        }
    }

    private void Freeze()
    {
        if (IsHeatingUp)
        {
            freezingness = 0f;
            StartCoroutine(dialogHandler.ShowAlert("The fireplace is burning bright. You feel warm.", 0f));
            return;
        }
        
        if (Time.time > freezingTimestamp)
        {
            freezingTimestamp = Time.time + freezingRate;
            if (freezingness >= MAX_FREEZING/2)
            {
                StartCoroutine(dialogHandler.ShowAlert("You feel cold.", 0f));
            }
            else
            {
                dialogHandler.HideAlert();
            }
            
            freezingness += freezingIncrement;
        }
    }

    private void AnimateSliderValue()
    {
        var currentFreeze = Mathf.SmoothDamp(freezingSlider.value, freezingness, ref freezingSliderVelocity,
            30 * Time.deltaTime);
        freezingSlider.value = currentFreeze;
        
        var currentMental = Mathf.SmoothDamp(mentalSlider.value, mental, ref mentalSliderVelocity,
            30 * Time.deltaTime);
        mentalSlider.value = currentMental;
    }
}
