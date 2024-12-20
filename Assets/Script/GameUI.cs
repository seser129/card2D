using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] Text turnResultText;
    [SerializeField] Text playerLifeText;
    [SerializeField] Text enemyLifeText;
    [SerializeField] GameObject resultPanel;
    [SerializeField] Text resultText;

    public void Init()
    {
        turnResultText.gameObject.SetActive(false);
        resultPanel.SetActive(false);
    }
    //Lifeを渡して表示
    public void ShowLifes(int playerLife,int enemyLife)
    {
        playerLifeText.text = $"x{playerLife}";
        enemyLifeText.text = $"x{enemyLife}";
    }

    //ターンの勝敗表示
    public void ShowTurnResult(string result)
    {
        turnResultText.gameObject.SetActive(true);
        turnResultText.text = result;
    }

    public void ShowGameResult(string result)
    {
        resultPanel.SetActive(true);
        resultText.text = result;
    }


    public void SetupNextTurn()
    {
        turnResultText.gameObject.SetActive(false);

    }


}
