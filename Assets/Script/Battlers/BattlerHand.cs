using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlerHand : MonoBehaviour
{
    List<Card> list = new List<Card>();

    //listに追加して自分の子要素にする
    public void Add(Card card)
    {
        list.Add(card);
        card.transform.SetParent(transform);//親として自分自身を登録する
    }

    public void Remove(Card card)
    {
        list.Remove(card);
    }

    //手札を整頓する(位置・間隔・順)
    public void ResetPosition()
    {
        //Sort:Numberの小さい順に並べる
        list.Sort((card0, card1) => card0.Base.Number - card1.Base.Number);

        for (int i = 0; i < list.Count; i++)
        {
            float posX = (i-list.Count/2) * 1.4f;
            list[i].transform.localPosition = new Vector3(posX, 0);
        }
    }

    //ランダムでカードを選ぶ
    public Card RandomRemove()
    {
        //ランダム
        int r = Random.Range(0, list.Count);
        Card card = list[r];
        //抜き取る
        Remove(card);
        return card;
    }


}
