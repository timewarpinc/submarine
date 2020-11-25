using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private UnityEngine.UI.Text message;
    [SerializeField] private float pausedTimeScale = 0.5f;
    void Start()
    {
        if (pausePanel == null) { pausePanel = new GameObject(); }
        pausePanel.SetActive(false);
    }


    public void WinGame(GameObject cause)
    {
        Debug.Log("You Win");
        PauseGame("You Win!");
    }

    public void LoseGame(GameObject cause)
    {
        Debug.Log("lose");
        PauseGame("You Lose!");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    private void PauseGame(String text)
    {
        Time.timeScale = pausedTimeScale;
        this.message.text = text;
        pausePanel.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }
    private void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        //enable the scripts again
    }
}
