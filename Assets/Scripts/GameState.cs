using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private float pausedTimeScale = 0.5f;
    void Start()
    {
        pausePanel = new GameObject();
        pausePanel.SetActive(false);
    }


    public void WinGame(GameObject cause)
    {
        Debug.Log("Win");
        PauseGame();
    }

    public void LoseGame(GameObject cause)
    {
        Debug.Log("lose");
        PauseGame();
    }

    private void PauseGame()
    {
        Time.timeScale = pausedTimeScale;
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
