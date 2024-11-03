using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battler : MonoBehaviour
{
    [SerializeField] BattlerHand hand;
    [SerializeField] SubmitPosition submitPosition;

    public BattlerHand Hand { get => hand;  }

    //���̃^�C�~���O��Battler�̓J�[�h��F���ł���
    //�����̊֐��ɓo�^���Ă���
    public void SetCardToHand(Card card)
    {
        hand.Add(card);
        card.OnClickCard = SelectedCard;
    }

    void SelectedCard(Card card)
    {
        //Debug.Log(card.Base.Number);
        //���ŃZ�b�g���Ă���΁A��D�ɖ߂�
        if (submitPosition.SubmitCard)
        {
            hand.Add(submitPosition.SubmitCard);
        }
        hand.Remove(card);//�Z�b�g����Ƃ��Ɏ�D�����菜��
        submitPosition.Set(card);//�Z�b�g����
        hand.ResetPosition();//�n���h�̈ʒu��߂�
    }
}
