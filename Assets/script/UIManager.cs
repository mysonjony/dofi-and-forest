using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // GameOverパネルオブジェクトを格納する変数
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] GameObject gameclearPanel;

    public void Start()
    {
        //シーンが読み込まれてこのオブジェクトが読み込まれた時に
        //プレイ中の状態にするのとパネルを非表示にする
        gameoverPanel.SetActive(false);
        gameclearPanel.SetActive(false);
        GameManager.Instance.isplaying = true;
    }

    public void GameOver()
    {
        //ゲームオーバーの際にパネルを表示させ、プレイ中ではないという状態にする
        gameoverPanel.SetActive(true);
        GameManager.Instance.isplaying = false;
        gameclearPanel = null;
        
    }

    public void GameClear()
    {
        gameclearPanel.SetActive(true);
        GameManager.Instance.isplaying = false;
        gameoverPanel = null;
    }

    public void titleButton()
    {
        SceneManager.LoadScene("titleScene");
    }

    public void restartButton()
    {
        SceneManager.LoadScene("gameMain");
    }
}
