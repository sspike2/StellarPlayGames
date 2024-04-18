using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using System;

public class GamePlayScreen : UIScreenBase
{


    private GameplayState gameplayState;

    [SerializeField]
    private Button[] choiceButtons;

    private Sprite[] currentlyEquippedSprites = new Sprite[5];

    [SerializeField]
    private Image AIChoiceImage;  

    [SerializeField]
    private GameObject ContinuePopUp;

    [SerializeField]
    private TextMeshProUGUI resultText;

    [SerializeField]
    private float cycleDuration = 0.1f; // AI image cycle duration

    [SerializeField]
    private int cycleCount = 10; // Total number of images shown

    [SerializeField]
    private ParticleSystem winParticles;


    private int currentResult;
   

    public override void Initialize()
    {
        base.Initialize();
        if (gameplayState == null)
            gameplayState = GameManager.Instance.GetCurrentState() as GameplayState;

        int i = 0;
        //assign skins to buttons
        foreach (Choices choice in Enum.GetValues(typeof(Choices)))
        {
            int itemID;
            SaveManager.Instance.CurrentEquippedItems.TryGetValue(choice, out itemID);
            Sprite sprite = GameManager.Instance.ShopItems[itemID].choiceIcon;
            currentlyEquippedSprites[i] = sprite;
            choiceButtons[i].image.sprite = sprite;
            i++;
        }


        Reset();
    }

    //Choice Button
    public void PlayerMadeChoice(int playerChoice)
    {
        int aiChoice;
        currentResult = gameplayState.GetResult(playerChoice, out aiChoice);
        StartCoroutine(CycleImages(aiChoice));
    }


    /// <summary>
    /// Animation for AI choice selection
    /// </summary>
    /// <param name="aiChoiceIndex"></param>
    /// <returns></returns>
    private IEnumerator CycleImages(int aiChoiceIndex)
    {
        
        int count = 0;
        AIChoiceImage.enabled = true;
        while (count < cycleCount)
        {
            // Cycle through each sprite
            foreach (var sprite in currentlyEquippedSprites)
            {
                AIChoiceImage.sprite = sprite;
                yield return new WaitForSeconds(cycleDuration);
                count++;
                if (count >= cycleCount)
                    break;
            }
        }       
        AIChoiceImage.sprite = currentlyEquippedSprites[aiChoiceIndex];
        DisplayResult();
    }

    private void DisplayResult()
    {
        ContinuePopUp.SetActive(true);

        switch (currentResult)
        {
            case 1:
                resultText.text = "You win!";
                winParticles.Play();
                EventManager.SendScoreUpdatedEvent(true);
                break;
            case 0:
                resultText.text = "It's a draw!";
                break;
            case -1:
                resultText.text = "You lose!";
                break;
        }
    }
    public void WinLossPopUpButtonPressed()
    {
        if (currentResult == -1)
        {
            GameManager.Instance.PushState(GameStates.GameOverState, true);
        }
        else
        {
            Reset();
        }
    }

    public void Reset()
    {
        ContinuePopUp.SetActive(false);
        AIChoiceImage.enabled = false;

    }

}
