using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAction : ObstableAction
{
    [SerializeField] private Coin[] coins;

    //Shows coins
    public override void action()
    {
        foreach (Coin coin in coins)
        {
            coin.showMesh();
        }
    }
}
