using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitPosition : MonoBehaviour
{
    //選択されたカードを管理する
    Card submitCard;

    public Card SubmitCard { get => submitCard; }

    //自分の子要素にする・位置を自分の場所にする
    public void Set(Card card)
    {
        submitCard = card;
        card.transform.SetParent(transform);//子要素
        card.transform.position = transform.position;//場所
    }

    public void DeleteCard()
    {
        Destroy(submitCard.gameObject);
        submitCard = null;
    }
}
