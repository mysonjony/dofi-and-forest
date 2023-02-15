using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //ゲーム全体を管理しやすくするためのもの
    //オブジェクト自体をstatic(静的)にすることで
    //どのシーンでもアクセスしやすくなる

    public static GameManager Instance = null;

    //現在プレイ中かどうかを格納する変数
    public bool isplaying = false;


    void Awake()
    {

        if (Instance == null)//シーン上にこの(スクリプトのついた)オブジェクトがない場合
        {
            //実体を変数に格納、シーンが変わっても破棄されないようにする
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else//既に存在する場合
        {
            //このオブジェクトを破壊する
            Destroy(this.gameObject);
        }
    }
}
