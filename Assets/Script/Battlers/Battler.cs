using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Battler : MonoBehaviour
{
    [SerializeField] BattlerHand hand;
    [SerializeField] SubmitPosition submitPosition;
    public bool IsSubmitted { get; private set; }
    public UnityAction OnSubmitAction;

    public BattlerHand Hand { get => hand;  }
    public Card SubmitCard { get => submitPosition.SubmitCard; }

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
        //決定されていたら何もしない
        if (IsSubmitted)
        {
            return;
        }
        //すでセットしていれば、手札に戻す
        if (submitPosition.SubmitCard)
        {
            hand.Add(submitPosition.SubmitCard);
        }
        hand.Remove(card);//セットするときに手札から取り除く
        submitPosition.Set(card);//セットする
        hand.ResetPosition();//ハンドの位置を戻す
    }

    public void OnSubmitButton()
    {
        //カードを出している時
        if (submitPosition.SubmitCard)
        {
            //カードの決定->変更はできない(決定ボタンを押せない/カードの交換葉できない)
            IsSubmitted = true;
            //GameMasterに通知
            OnSubmitAction?.Invoke();
        }
    }

    public void RandomSubmit()
    {
        //手札からランダムでカードを抜き取る
        Card card = hand.RandomRemove();
        //提出用にセット
        submitPosition.Set(card);//セットする
        //カードの決定
        IsSubmitted = true;
        //提出GameMasterに通知する
        OnSubmitAction?.Invoke();
        hand.ResetPosition();
    }

    public void SetupNextTurn()
    {
        IsSubmitted = false;
        submitPosition.DeleteCard();
    }

}
