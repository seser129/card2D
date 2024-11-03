using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] Battler player;
    [SerializeField] Battler enemy;
    [SerializeField] CardGenerator cardGenerator;//カードの生成に必用

    private void Start()
    {
        Setup();
    }
    //カードを生成して配る
    void Setup()
    {
        SendCardsTo(player);//自分
        SendCardsTo(enemy);//敵
    }
    //カードを生成関数
    void SendCardsTo(Battler battler)
    {
        for (int i = 0; i < 8; i++)
        {
            Card card = cardGenerator.Spawn(i);
            //battler.Hand.Add(card);//BattlerHandに直接渡す
            battler.SetCardToHand(card);//一度battler内の関数に渡す
        }
        battler.Hand.ResetPosition();
    }

}
