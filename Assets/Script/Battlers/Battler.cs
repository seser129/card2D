using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battler : MonoBehaviour
{
    [SerializeField] BattlerHand hand;
    [SerializeField] SubmitPosition submitPosition;

    public BattlerHand Hand { get => hand;  }

    //このタイミングでBattlerはカードを認識できる
    //自分の関数に登録しておく
    public void SetCardToHand(Card card)
    {
        hand.Add(card);
        card.OnClickCard = SelectedCard;
    }

    void SelectedCard(Card card)
    {
        //Debug.Log(card.Base.Number);
        //すでセットしていれば、手札に戻す
        if (submitPosition.SubmitCard)
        {
            hand.Add(submitPosition.SubmitCard);
        }
        hand.Remove(card);//セットするときに手札から取り除く
        submitPosition.Set(card);//セットする
        hand.ResetPosition();//ハンドの位置を戻す
    }
}
