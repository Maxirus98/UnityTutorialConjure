using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : Interactable
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;
    public Inventory inventory;
    
    private bool m_IsPlayerAtExit;
    private bool m_zeroMental;
    private float m_Timer;
    private bool m_HasAudioPlayed;

    void Update()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if(m_zeroMental)
        {
            EndLevel (caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }
    
    public override void DoSomething()
    {
        if (inventory.HasItem(item.itemName))
        {
            m_IsPlayerAtExit = true;
        }
    }

    public void HandleZeroMental ()
    {
        m_zeroMental = true;
    }

    private void EndLevel (CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if(!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }
        
        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;
        
        if(m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
