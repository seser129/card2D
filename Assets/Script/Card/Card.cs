using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;//関数を登録する
using UnityEngine.UI;

public class Card : MonoBehaviour
{

    //カードUI
    //ゲーム内の処理
    [SerializeField] Text nameText;
    [SerializeField] Text numberText;
    [SerializeField] Image icon;
    [SerializeField] Text descriptionText;
    public CardBase Base { get; private set; }//CardBaseを取得は外でもできるが設定するのは中だけ
    //関数を登録する（管理していないものの関数を使える）
    public UnityAction<Card> OnClickCard;

    public void Set(CardBase cardBase)
    {
        Base = cardBase;
        nameText.text = cardBase.Name;
        numberText.text = cardBase.Number.ToString();
        icon.sprite = cardBase.Icon;
        descriptionText.text = cardBase.Description;
    }

    public void OnClick()
    {
        //Debug.Log("Battlerへ通知");
        OnClickCard?.Invoke(this);//登録してる関数(OnClickCard)を実行する
    }

}
