using System;
using Unity.UIElements.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class GameState : MonoBehaviour
{
    [SerializeField] public PanelRenderer m_EndGameScreen;

    private Label message;

    private void Start()
    {
        m_EndGameScreen.enabled = false;

        var button = m_EndGameScreen.visualTree.Q<Button>("Retry");

        button.RegisterCallback<PointerUpEvent>(OnRetry);
        button.RegisterCallback<MouseUpEvent>(OnRetry);

        button.RegisterCallback<PointerDownEvent>(OnRetry);
        button.RegisterCallback<MouseDownEvent>(OnRetry);

        message = m_EndGameScreen.visualTree.Q<Label>("Message");
    }

    private void OnRetry(IMouseEvent evt)
    {
        throw new System.NotImplementedException();
    }

    private void OnRetry(IPointerEvent evt)
    {
        throw new System.NotImplementedException();
    }


    public void WinGame(GameObject cause)
    {
        Debug.Log("Win");
        PauseGame();
        ShowUI("You Win!");
    }

    public void LoseGame(GameObject cause)
    {

        Debug.Log("lose");
        PauseGame();
    }

    private void ShowUI(String text)
    {
        message.text = text;
        m_EndGameScreen.enabled = true;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        //Disable scripts that still work while timescale is set to 0
    }
    private void ContinueGame()
    {
        Time.timeScale = 1;
        //enable the scripts again
    }
}
