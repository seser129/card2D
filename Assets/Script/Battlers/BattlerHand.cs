using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlerHand : MonoBehaviour
{
    List<Card> list = new List<Card>();

    //list�ɒǉ����Ď����̎q�v�f�ɂ���
    public void Add(Card card)
    {
        list.Add(card);
        card.transform.SetParent(transform);//�e�Ƃ��Ď������g��o�^����
    }

    public void Remove(Card card)
    {
        list.Remove(card);
    }

    //��D�𐮓ڂ���(�ʒu�E�Ԋu�E��)
    public void ResetPosition()
    {
        //Sort:Number�̏��������ɕ��ׂ�
        list.Sort((card0, card1) => card0.Base.Number - card1.Base.Number);

        for (int i = 0; i < list.Count; i++)
        {
            float posX = (i-list.Count/2) * 1.4f;
            list[i].transform.localPosition = new Vector3(posX, 0);
        }
    }

    //�����_���ŃJ�[�h��I��
    public Card RandomRemove()
    {
        //�����_��
        int r = Random.Range(0, list.Count);
        Card card = list[r];
        //�������
        Remove(card);
        return card;
    }


}
