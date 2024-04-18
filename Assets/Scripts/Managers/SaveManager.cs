using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

/// <summary>
/// Choice type 
/// </summary>
public enum Choices
{
    Rock = 0,
    Paper = 1,
    Scissor = 2,
    Lizard = 3,
    Spock = 4,
}
public class SaveManager : Singleton<SaveManager>
{
    // List of unlocked items
    private bool[] shopUnlocks;
    private int highScore; // High score saved
    private int coins; // coins saved
    private int score; // current score // not saved

    // keypair for currently equipped skins
    private Dictionary<Choices, int> currentEquippedItems = new Dictionary<Choices, int>();

    private readonly string shopSaveString = "ShopSaveString";
    private readonly string highscoreSaveString = "HighScoreSaveString";
    private readonly string coinsSaveString = "CoinsSaveString";



    public int Score { get { return score; } }
    public int HighScore { get { return highScore; } }
    public int Coins { get { return coins; } }

    public Dictionary<Choices, int> CurrentEquippedItems { get { return currentEquippedItems; } }
    public bool[] ShopUnlocks { get { return shopUnlocks; } }


    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    private void OnEnable()
    {
        EventManager.ScoreUpdated += ScoreUpdatedListener;
    }

    private void OnDisable()
    {
        EventManager.ScoreUpdated -= ScoreUpdatedListener;
    }
    private void ScoreUpdatedListener(bool addScore)
    {
        if (addScore)
        {
            score += 1;
        }
        else
        {
            score = 0;
        }
        EventManager.SendUpdateScoreUI(score);
    }


    public void UpdateHighScore()
    {
        if (score < highScore)
            return;
        highScore = score;
        SaveScore();
    }
    public void UpdateCoins()
    {
        coins += score * 10;
        SaveScore();
    }

    public void CheatCoinsAdd()
    {
        coins += 10000;
        SaveScore();
    }

    public void ItemPurchased(int index, int cost)
    {
        coins -= cost;
        shopUnlocks[index] = true;
    }


    public void SaveAll()
    {
        SaveShop();
        SaveScore();
    }

    /// <summary>
    /// Save method only for shop elements
    /// </summary>
    public void SaveShop()
    {
        SaveData saveData = new SaveData(shopUnlocks, currentEquippedItems);
        PlayerPrefs.SetString(shopSaveString, saveData.ToJSON());
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Save method for score and coins
    /// </summary>
    public void SaveScore()
    {

        PlayerPrefs.SetInt(coinsSaveString, coins);
        PlayerPrefs.SetInt(highscoreSaveString, highScore);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        string json = PlayerPrefs.GetString(shopSaveString);

        // Save data does not exist
        // so create new save data
        if (json.Length == 0)
        {
            shopUnlocks = new bool[15];
            coins = 0;
            highScore = 0;
            int j = 0;
            foreach (Choices choice in Enum.GetValues(typeof(Choices)))
            {
                //equip default skins. gap is based on no of skins per category
                currentEquippedItems.Add(choice, GameManager.Instance.ItemsPerCategory * j);
                shopUnlocks[GameManager.Instance.ItemsPerCategory * j] = true; // unlock default skins
                j++;
            }

            return;
        }

        SaveData savedData = SaveData.FromJson(json);
        shopUnlocks = savedData.shopUnlocks;

        //save the values to dictionary
        for (int i = 0; i < savedData.currentlyEquippedItems.Length; i++)
        {
            currentEquippedItems.Add((Choices)i, savedData.currentlyEquippedItems[i]);
        }
        coins = PlayerPrefs.GetInt(coinsSaveString);
        highScore = PlayerPrefs.GetInt(highscoreSaveString);
    }

    private void OnApplicationQuit()
    {
        SaveAll();
    }



}
#region SerializedSaveClass
/// <summary>
/// Save Class
/// </summary>
[System.Serializable]
public class SaveData
{
    public bool[] shopUnlocks;
    public int[] currentlyEquippedItems;

    public SaveData(bool[] shopUnlocks, Dictionary<Choices, int> currentlyEquippedItems)
    {
        this.shopUnlocks = shopUnlocks;
        this.currentlyEquippedItems = new int[5];

        int i = 0;
        foreach (var item in currentlyEquippedItems)
        {
            this.currentlyEquippedItems[i] = (int)item.Value;
            i++;
        }

    }

    public static SaveData FromJson(string json)
    {
        return JsonUtility.FromJson<SaveData>(json);
    }

    public string ToJSON()
    {
        return JsonUtility.ToJson(this);
    }
}
#endregion