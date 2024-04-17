using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private bool[] shopUnlocks;
    private int highScore;
    private int coins;

    private int[] currentEquippedItems;

    private readonly string shopSaveString = "ShopSaveString";
    private readonly string highscoreSaveString = "HighScoreSaveString";
    private readonly string coinsSaveString = "CoinsSaveString";




    public int HighScore { get { return highScore; } }
    public int Coins { get { return coins; } }

    public int[] CurrentEquippedItems { get { return currentEquippedItems; } }
    public bool[] ShopUnlocks { get { return shopUnlocks; } }





    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        Load();
    }


    public void SaveAll()
    {
        SaveShop();
        SaveScore();
    }

    public void SaveShop()
    {
        SaveData saveData = new SaveData(shopUnlocks, currentEquippedItems);
        PlayerPrefs.SetString(shopSaveString, saveData.ToJSON());
        PlayerPrefs.Save();
    }


    public void SaveScore()
    {

        PlayerPrefs.SetInt(coinsSaveString, coins);
        PlayerPrefs.SetInt(highscoreSaveString, highScore);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        LoadJson();
    }


    private void LoadJson()
    {
        string json = PlayerPrefs.GetString(shopSaveString);
        Debug.Log(json);

        // Save data does not exist
        if (json.Length == 0)
        {
            shopUnlocks = new bool[15];
            currentEquippedItems = new int[15];
            coins = 0;
            highScore = 0;

            for (int i = 0; i < 5; i++)
            {
                currentEquippedItems[i] = i * GameManager.Instance.ItemsPerCategory;
            }

            for (int i = 0; i < 5; i++)
            {
                shopUnlocks[currentEquippedItems[i]] = true;
            }

            return;
        }

        SaveData savedData = SaveData.FromJson(json);
        shopUnlocks = savedData.shopUnlocks;
        currentEquippedItems = savedData.currentlyEquippedItems;

        coins = PlayerPrefs.GetInt(coinsSaveString);
        highScore = PlayerPrefs.GetInt(highscoreSaveString);

    }

}


[System.Serializable]
public class SaveData
{
    public bool[] shopUnlocks;
    public int[] currentlyEquippedItems;

    public SaveData(bool[] shopUnlocks, int[] currentlyEquippedItems)
    {
        this.shopUnlocks = shopUnlocks;
        this.currentlyEquippedItems = currentlyEquippedItems;
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
