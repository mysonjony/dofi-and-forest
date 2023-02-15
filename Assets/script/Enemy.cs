using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// シーン変更用
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    // [Header("画面外でも行動する")] public bool nonVisibleAct;

    #region//インスペクターで設定する
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    // [Header("画面外でも行動する")] public float nonVisibleAct;
    #endregion

    private Rigidbody2D rb = null;
    // カメラ内処理用の変数を入れる
    private SpriteRenderer sr = null;


    // 敵キャラの待ち時間
    int WaitTime;

    private GameObject PlayerObj = null;


    // Start is called before the first frame update
    void Start()
    {
        PlayerObj = GameObject.Find("player");
        // rightTleftF = transform;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        // ランダムに120～600フレームの待ち時間を指定
        WaitTime = Random.Range(30, 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isplaying == true && sr.isVisible) // if (sr.isVisible || nonVisibleAct) 画面に映っていなくても動く場合
        {

            if (transform.position.x < PlayerObj.transform.position.x)
            {
                // 待ち時間をカウントダウン
                WaitTime = WaitTime - 1;
                // カウントダウンが終了したら
                if (WaitTime == 0)
                {
                    // 敵にジャンプさせる(右方向)
                    rb.AddForce(new Vector2(5000.0f, 500.0f));

                    // 改めて待ち時間を設定する
                    WaitTime = Random.Range(50, 400);
                }

            }
            else if (transform.position.x > PlayerObj.transform.position.x)
            {
                // 待ち時間をカウントダウン
                WaitTime--;
                // カウントダウンが終了したら
                if (WaitTime == 0)
                {
                    // 敵にジャンプさせる(左方向)
                    rb.AddForce(new Vector2(-5000.0f, 500.0f));

                    // 改めて待ち時間を設定する
                    WaitTime = Random.Range(50, 400);
                }
            }
            // rb.velocity = new Vector2(xVector * speed, -gravity);

        }
        else
        {
            // rb.Sleep();
        }
    }

}