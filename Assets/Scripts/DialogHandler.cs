using System.Collections;
using TMPro;
using UnityEngine;

public class DialogHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageUi;
    
    public IEnumerator ShowAlert(string message, float timeShown = 2f)
    {
        if (!messageUi.gameObject.activeInHierarchy)
        {
            messageUi.gameObject.SetActive(true);
        }
        
        messageUi.text = message;

        if (timeShown > 0f)
        {
            yield return new WaitForSeconds(timeShown);
            HideAlert();
        }
    }
    
    public void HideAlert()
    {
        if (messageUi.gameObject.activeInHierarchy)
        {
            messageUi.gameObject.SetActive(false);
        }
    }
}
