using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;//�֐���o�^����
using UnityEngine.UI;

public class Card : MonoBehaviour
{

    //�J�[�hUI
    //�Q�[�����̏���
    [SerializeField] Text nameText;
    [SerializeField] Text numberText;
    [SerializeField] Image icon;
    [SerializeField] Text descriptionText;
    public CardBase Base { get; private set; }//CardBase���擾�͊O�ł��ł��邪�ݒ肷��̂͒�����
    //�֐���o�^����i�Ǘ����Ă��Ȃ����̂̊֐����g����j
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
        //Debug.Log("Battler�֒ʒm");
        OnClickCard?.Invoke(this);//�o�^���Ă�֐�(OnClickCard)�����s����
    }

}
