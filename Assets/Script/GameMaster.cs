using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] Battler player;
    [SerializeField] Battler enemy;
    [SerializeField] CardGenerator cardGenerator;//カードの生成に必用
    [SerializeField] GameObject submitButton;
    [SerializeField] GameUI gameUI;
    RuleBook ruleBook;
    //同じオブジェクト内なのでGetComponentで取得出来る
    private void Awake()
    {
        ruleBook = GetComponent<RuleBook>();
    }

    private void Start()
    {
        Setup();
    }
    //カードを生成して配る
    void Setup()
    {
        gameUI.Init();
        player.Life = 4;
        enemy.Life = 4;
        gameUI.ShowLifes(player.Life, enemy.Life);

        player.OnSubmitAction = SubmittdAction;
        enemy.OnSubmitAction = SubmittdAction;
        SendCardsTo(player);//自分
        SendCardsTo(enemy);//敵
    }

    void SubmittdAction()
    {
        //Debug.Log("SubmittedAction");
        if (player.IsSubmitted && enemy.IsSubmitted)//両方提出
        {
            submitButton.SetActive(false);//playerが提出したら
            //Cardの勝利判定
            StartCoroutine(CardsBattle());//コルーチン有り(必須)
            //CardBattle();//コルーチン無し
        }
        else if (player.IsSubmitted)//Playerのみ提出
        {
            submitButton.SetActive(false);//playerが提出したら

            //enemyからカードを出す
            enemy.RandomSubmit();
        }
        else if (enemy.IsSubmitted)//enemyのみ提出
        {
            //Playerの提出を待つ
        }
    }


    //カードを生成関数
    void SendCardsTo(Battler battler)
    {
        for (int i = 0; i < 8; i++)
        {
            Card card = cardGenerator.Spawn(i);
            //battler.Hand.Add(card);//BattlerHandに直接渡す
            battler.SetCardToHand(card);//一度battler内の関数に渡す
        }
        battler.Hand.ResetPosition();
    }

    //Cardの勝利判定
    //少し遅らせてから結果を表示：コルーチン(IEnumerator)
    //表示が終わったら、次のターンに移る(場のカードを捨てる)
    IEnumerator CardsBattle()
    {
        yield return new WaitForSeconds(1f);//１秒処理を止める＜コルーチン＞
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
        yield return new WaitForSeconds(1f);//１秒処理を止める

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
        //姫
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



        //勝敗パネル表示
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

    //表示が終わったら、次のターンに移る(場のカードを捨てる)
    void SetupNextTurn()
    {
        player.SetupNextTurn();
        enemy.SetupNextTurn();
        gameUI.SetupNextTurn();
        submitButton.SetActive(true);
    }


}
