using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScreen : UIScreenBase
{
    [SerializeField]
    private TextMeshProUGUI newHighScoreText;

    [SerializeField]
    private TextMeshProUGUI coinsText;

    [SerializeField]
    private TextMeshProUGUI highScoreText;

    /// <summary>
    /// Inititalize Text for game over screen. 
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();
        newHighScoreText.enabled = false;
        EventManager.SendUpdateScoreUI(SaveManager.Instance.Score);

        if (SaveManager.Instance.Score > SaveManager.Instance.HighScore)
        {
            newHighScoreText.enabled = true;
            SaveManager.Instance.UpdateHighScore();
        }

        highScoreText.text = $"HighScore: {SaveManager.Instance.HighScore}";
        coinsText.text = $"Coins: {SaveManager.Instance.Coins}";

    }
    public void RestartButton()
    {
        GameManager.Instance.PushState(GameStates.GameplayState, true);
    }

    public void MainMenuButton()
    {
        GameManager.Instance.PopState();
    }
}
