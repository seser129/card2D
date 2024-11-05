using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] Battler player;
    [SerializeField] Battler enemy;
    [SerializeField] CardGenerator cardGenerator;//�J�[�h�̐����ɕK�p
    [SerializeField] GameObject submitButton;
    [SerializeField] GameUI gameUI;
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
        gameUI.Init();
        player.Life = 4;
        enemy.Life = 4;
        gameUI.ShowLifes(player.Life, enemy.Life);

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
        //Debug.Log(result);
        switch (result)
        {
            case Result.TurnWin:
            case Result.GameWin:
                gameUI.ShowTurnResult("WIN");
                enemy.Life--;
                break;
            case Result.TurnWin2:
                gameUI.ShowTurnResult("WIN");
                enemy.Life-=2;
                break;
            case Result.TurnLose:
            case Result.GameLose:
                gameUI.ShowTurnResult("LOSE");
                player.Life--;
                break;
            case Result.TurnLose2:
                gameUI.ShowTurnResult("LOSE");
                player.Life -= 2;
                break;
            case Result.TurnDraw:
                gameUI.ShowTurnResult("Draw");
                break;
        }
        gameUI.ShowLifes(player.Life, enemy.Life);
        yield return new WaitForSeconds(1f);//�P�b�������~�߂�

        if (player.Life <= 0 || enemy.Life <= 0)
        {
            ShowResult(result);
        }
        else
        {
            SetupNextTurn();
        }
        
        //Debug.Log($"player.Life{player.Life},enemy.Life{enemy.Life}");
    }

    void ShowResult(Result result)
    {
        //�P
        if(result == Result.GameWin)
        {
            gameUI.ShowGameResult("WIN");
        }
        if (result == Result.GameLose)
        {
            gameUI.ShowGameResult("LOSE");
        }

        //LIfe
        if (player.Life <= 0 && enemy.Life <= 0)
        {
            gameUI.ShowGameResult("Draw");
        }
        else if (player.Life <= 0)
        {
            gameUI.ShowGameResult("LOSE");
        }
        else if (enemy.Life <= 0)
        {
            gameUI.ShowGameResult("WIN");
        }



        //���s�p�l���\��
        /*switch (result)
        {
            case Result.GameWin:
                gameUI.ShowGameResult("WIN");
                break;
            case Result.GameLose:
                gameUI.ShowGameResult("LOSE");
                break;
            case Result.GameDraw:
                gameUI.ShowGameResult("Draw");
                break;
        }*/
    }

    //�\�����I�������A���̃^�[���Ɉڂ�(��̃J�[�h���̂Ă�)
    void SetupNextTurn()
    {
        player.SetupNextTurn();
        enemy.SetupNextTurn();
        gameUI.SetupNextTurn();
        submitButton.SetActive(true);
    }


}
