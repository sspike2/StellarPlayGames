using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayScreen : MonoBehaviour
{


    private GameplayState gameplayState;

    [SerializeField]
    private Button[] choiceButtons;

    [SerializeField]
    private Sprite[] currenttlyEquipedSprites;

    [SerializeField]
    private Image AIChoiceImage;

    [SerializeField]
    private Image PlayerChoiceImage;

    [SerializeField]
    private Transform playerChoiceFinalPos;


    [SerializeField]
    private float cycleDuration = 0.1f; // Time between image changes

    [SerializeField]
    private int cycleCount = 10; // Total number of images shown before stopping


    // Start is called before the first frame update
    void Start()
    {
        gameplayState = GameManager.Instance.GetCurrentState() as GameplayState;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerMadeChoice(int playerChoice)
    {
        int aiChoice;
        int result = gameplayState.GetResult(playerChoice, out aiChoice);
        StartCoroutine(CycleImages(aiChoice));
        DisplayResult(result);
    }

    private IEnumerator CycleImages(int aiChoiceIndex)
    {
        int count = 0;
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
    }

    private void DisplayResult(int result)
    {
        switch (result)
        {
            case 1:
                //resultText.text = "You win!";
                Debug.Log("You win");
                break;
            case 0:
                //resultText.text = "It's a draw!";
                Debug.Log("It's a draw!");
                break;
            case -1:
                //resultText.text = "You lose!";
                Debug.Log("You Lose");
                break;
        }
    }
}
