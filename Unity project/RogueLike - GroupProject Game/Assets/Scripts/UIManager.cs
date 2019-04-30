using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    //The text that displays the stat's cost
    public Text strText;
    public Text defText;
    public Text vitText;
    public GameObject shopReference;

    public Button attackButton;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    public void BuyStr()
    {
        if (GameManager.instance.player.BuyStats("Str"))
        {
            Shop.strPriceMultiplier++;
            GameManager.instance.player.UpdateShopText();
        }
    }
    
    public void BuyDef()
    {
        if (GameManager.instance.player.BuyStats("Def"))
        {
            Shop.defPriceMultiplier++;
            GameManager.instance.player.UpdateShopText();
        }
    }

    public void ButVit()
    {
        if (GameManager.instance.player.BuyStats("Vit"))
        {
            Shop.vitPriceMultiplier++;
            GameManager.instance.player.UpdateShopText();
        }
    }

    public void ToggleAttack()
    {
        GameManager.instance.player.toggleAttack = !GameManager.instance.player.toggleAttack;
    }
}
