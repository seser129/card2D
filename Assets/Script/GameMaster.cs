using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] Battler player;
    [SerializeField] Battler enemy;
    [SerializeField] CardGenerator cardGenerator;//カードの生成に必用
    [SerializeField] GameObject submitButton;
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
        Debug.Log(result);
        yield return new WaitForSeconds(1f);//１秒処理を止める
        SetupNextTurn();
    }
    //表示が終わったら、次のターンに移る(場のカードを捨てる)
    void SetupNextTurn()
    {
        player.SetupNextTurn();
        enemy.SetupNextTurn();
        submitButton.SetActive(true);
    }


}
