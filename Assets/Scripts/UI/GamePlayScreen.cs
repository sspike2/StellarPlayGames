using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.FullSerializer;

public class GamePlayScreen : UIScreenBase
{


    private GameplayState gameplayState;

    [SerializeField]
    private Button[] choiceButtons;

    private Sprite[] currenttlyEquipedSprites = new Sprite[5];

    [SerializeField]
    private Image AIChoiceImage;

    [SerializeField]
    private Image PlayerChoiceImage;

    [SerializeField]
    private Transform playerChoiceFinalPos;

    [SerializeField]
    private GameObject ContinuePopUp;

    [SerializeField]
    private TextMeshProUGUI resultText;

    [SerializeField]
    private float cycleDuration = 0.1f; // Time between image changes

    [SerializeField]
    private int cycleCount = 10; // Total number of images shown before stopping

    [SerializeField]
    private ParticleSystem winParticles;


    private int currentResult;
    // Start is called before the first frame update
    void Start()
    {
        gameplayState = GameManager.Instance.GetCurrentState() as GameplayState;
        for (int i = 0; i < 5; i++)
        {
            int itemID = SaveManager.Instance.CurrentEquippedItems[i];
            Sprite sprite = GameManager.Instance.ShopItems[itemID].choiceIcon;
            currenttlyEquipedSprites[i] = sprite;
            choiceButtons[i].image.sprite = sprite;
        }
        //Reset();
    }

    public override void Initialize()
    {
        base.Initialize();
        Reset();
    }


    public void PlayerMadeChoice(int playerChoice)
    {
        int aiChoice;
        currentResult = gameplayState.GetResult(playerChoice, out aiChoice);
        StartCoroutine(CycleImages(aiChoice));
    }

    private IEnumerator CycleImages(int aiChoiceIndex)
    {
        int count = 0;
        AIChoiceImage.enabled = true;
        while (count < cycleCount)
        {
            // Cycle through each sprite
            foreach (var sprite in currenttlyEquipedSprites)
            {
                AIChoiceImage.sprite = sprite;
                yield return new WaitForSeconds(cycleDuration);
                count++;
                if (count >= cycleCount)
                    break;
            }
        }
        //Optionally set the final AI choice here, for example:
        AIChoiceImage.sprite = currenttlyEquipedSprites[aiChoiceIndex];
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
                EventManager.SendScoreUpdatedEvent();
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
