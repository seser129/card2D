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
        //���肳��Ă����牽�����Ȃ�
        if (IsSubmitted)
        {
            return;
        }
        //���ŃZ�b�g���Ă���΁A��D�ɖ߂�
        if (submitPosition.SubmitCard)
        {
            hand.Add(submitPosition.SubmitCard);
        }
        hand.Remove(card);//�Z�b�g����Ƃ��Ɏ�D�����菜��
        submitPosition.Set(card);//�Z�b�g����
        hand.ResetPosition();//�n���h�̈ʒu��߂�
    }

    public void OnSubmitButton()
    {
        //�J�[�h���o���Ă��鎞
        if (submitPosition.SubmitCard)
        {
            //�J�[�h�̌���->�ύX�͂ł��Ȃ�(����{�^���������Ȃ�/�J�[�h�̌����t�ł��Ȃ�)
            IsSubmitted = true;
            //GameMaster�ɒʒm
            OnSubmitAction?.Invoke();
        }
    }

    public void RandomSubmit()
    {
        //��D���烉���_���ŃJ�[�h�𔲂����
        Card card = hand.RandomRemove();
        //��o�p�ɃZ�b�g
        submitPosition.Set(card);//�Z�b�g����
        //�J�[�h�̌���
        IsSubmitted = true;
        //��oGameMaster�ɒʒm����
        OnSubmitAction?.Invoke();
        hand.ResetPosition();
    }

    public void SetupNextTurn()
    {
        IsSubmitted = false;
        submitPosition.DeleteCard();
    }

}
