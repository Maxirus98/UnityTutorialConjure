using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menuUi;
    
    private bool isVisible = false;
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ResumeGame()
    {
        ToggleMenu();
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    private void ToggleMenu()
    {
        isVisible = !isVisible;
        menuUi.SetActive(isVisible);
    }
}
