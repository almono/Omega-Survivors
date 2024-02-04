using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public static CoinController instance;
    public Coin coin;
    public int currentCoins;
    public Transform pickupsHolder;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;
        UIController.instance.UpdateCoins();
        SFXManager.instance.PlaySFXPitched(2);
    }

    public void SpendCoins(int amount)
    {
        currentCoins -= amount;
        UIController.instance.UpdateCoins();
    }

    public void DropCoin(Vector3 position, int coinValue)
    {
        // make sure coins wont overlap xp drop
        Coin newCoin = Instantiate(coin, position + new Vector3(.2f, .1f, 0f), Quaternion.identity, pickupsHolder);
        newCoin.coinAmount = coinValue;
        newCoin.gameObject.SetActive(true);
    }
}
