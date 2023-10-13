using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    public GameObject MainMenuPanel;
    //public GameObject LevelSelectPanel;
    //public GameObject Level1InfoPage;
    public TMP_Text timerText;
    float timer;

    private void Start()
    {
        MainMenuPanel.SetActive(true);
        if (PlayerPrefs.HasKey("timer"))
        {
            timer = PlayerPrefs.GetFloat("timer");
        }
        UpdateTimerDisplay();

    }
    public void QuitGame()
    {
        Debug.Log("QUIT GAME");
        Application.Quit();
    }
    //public void Gotolevelselect()
    //{
    //    MainMenuPanel.SetActive(false);
    //    LevelSelectPanel.SetActive(true);
    //}
    //public void GotoMenu()
    //{
    //    MainMenuPanel.SetActive(true);
    //    LevelSelectPanel.SetActive(false);
        
    //}


    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
    private void UpdateTimerDisplay()
    {
        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        // Update the UI Text
        
            timerText.text = string.Format("Your last run time: {0:D2}:{1:D2}", minutes, seconds);
        
       
        
    }
}
   
   