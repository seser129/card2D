using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] Battler player;
    [SerializeField] Battler enemy;
    [SerializeField] CardGenerator cardGenerator;//�J�[�h�̐����ɕK�p
    [SerializeField] GameObject submitButton;
    RuleBook ruleBook;
    //�����I�u�W�F�N�g���Ȃ̂�GetComponent�Ŏ擾�o����
    private void Awake()
    {
        ruleBook = GetComponent<RuleBook>();
    }

    private void Start()
    {
        Setup();
    }
    //�J�[�h�𐶐����Ĕz��
    void Setup()
    {
        player.OnSubmitAction = SubmittdAction;
        enemy.OnSubmitAction = SubmittdAction;
        SendCardsTo(player);//����
        SendCardsTo(enemy);//�G
    }

    void SubmittdAction()
    {
        //Debug.Log("SubmittedAction");
        if (player.IsSubmitted && enemy.IsSubmitted)//������o
        {
            submitButton.SetActive(false);//player����o������
            //Card�̏�������
            StartCoroutine(CardsBattle());//�R���[�`���L��(�K�{)
            //CardBattle();//�R���[�`������
        }
        else if (player.IsSubmitted)//Player�̂ݒ�o
        {
            submitButton.SetActive(false);//player����o������

            //enemy����J�[�h���o��
            enemy.RandomSubmit();
        }
        else if (enemy.IsSubmitted)//enemy�̂ݒ�o
        {
            //Player�̒�o��҂�
        }
    }


    //�J�[�h�𐶐��֐�
    void SendCardsTo(Battler battler)
    {
        for (int i = 0; i < 8; i++)
        {
            Card card = cardGenerator.Spawn(i);
            //battler.Hand.Add(card);//BattlerHand�ɒ��ړn��
            battler.SetCardToHand(card);//��xbattler���̊֐��ɓn��
        }
        battler.Hand.ResetPosition();
    }

    //Card�̏�������
    //�����x�点�Ă��猋�ʂ�\���F�R���[�`��(IEnumerator)
    //�\�����I�������A���̃^�[���Ɉڂ�(��̃J�[�h���̂Ă�)
    IEnumerator CardsBattle()
    {
        yield return new WaitForSeconds(1f);//�P�b�������~�߂遃�R���[�`����
        Result result = ruleBook.GetResult(player, enemy);
        Debug.Log(result);
        yield return new WaitForSeconds(1f);//�P�b�������~�߂�
        SetupNextTurn();
    }
    //�\�����I�������A���̃^�[���Ɉڂ�(��̃J�[�h���̂Ă�)
    void SetupNextTurn()
    {
        player.SetupNextTurn();
        enemy.SetupNextTurn();
        submitButton.SetActive(true);
    }


}
