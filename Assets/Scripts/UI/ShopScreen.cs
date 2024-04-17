using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScreen : UIScreenBase
{



    [SerializeField]
    private ShopIcon[] shopIcons;

   

    // Start is called before the first frame update
    void Start()
    {

    }
    public override void Initialize()
    {
        base.Initialize();
        for (int i = 0; i < shopIcons.Length; i++)
        {
            shopIcons[i].SetupIcon(GameManager.Instance.ShopItems[i], i);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
