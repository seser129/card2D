using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlerHand : MonoBehaviour
{
    List<Card> list = new List<Card>();

    //list‚É’Ç‰Á‚µ‚ÄŽ©•ª‚ÌŽq—v‘f‚É‚·‚é
    public void Add(Card card)
    {
        list.Add(card);
        card.transform.SetParent(transform);//e‚Æ‚µ‚ÄŽ©•ªŽ©g‚ð“o˜^‚·‚é
    }

    public void Remove(Card card)
    {
        list.Remove(card);
    }

    //ŽèŽD‚ð®“Ú‚·‚é(ˆÊ’uEŠÔŠuE‡)
    public void ResetPosition()
    {
        //Sort:Number‚Ì¬‚³‚¢‡‚É•À‚×‚é
        list.Sort((card0, card1) => card0.Base.Number - card1.Base.Number);

        for (int i = 0; i < list.Count; i++)
        {
            float posX = (i-list.Count/2) * 1.4f;
            list[i].transform.localPosition = new Vector3(posX, 0);
        }
    }
}
