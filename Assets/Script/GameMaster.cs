using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] Battler player;
    [SerializeField] Battler enemy;
    [SerializeField] CardGenerator cardGenerator;//�J�[�h�̐����ɕK�p

    private void Start()
    {
        Setup();
    }
    //�J�[�h�𐶐����Ĕz��
    void Setup()
    {
        SendCardsTo(player);//����
        SendCardsTo(enemy);//�G
    }
    //�J�[�h�𐶐��֐�
    void SendCardsTo(Battler battler)
    {
        for (int i = 0; i < 8; i++)
        {
            Card card = cardGenerator.Spawn(i);
            battler.Hand.Add(card);
        }
        battler.Hand.ResetPosition();
    }

}
