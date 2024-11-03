using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    [SerializeField] CardBase[] CardBases;
    [SerializeField] Card CardPrefab;

    /*private void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            Spawn(i);
        }
    }*/

    //ƒJ[ƒh‚Ì¶¬
    public Card Spawn(int number)
    {
        Card card = Instantiate(CardPrefab);
        card.Set(CardBases[number]);
        return card;

        
    }

}
