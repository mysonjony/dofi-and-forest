using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// シーン変更用
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 何か＝主人公に当たった場合
    void OnCollisionEnter2D(Collision2D col)
    {
        // ゲームオーバー用シーンを読み込んでシーン変更
        // SceneManager.LoadScene("GameOver");
        // 当たった相手のタグを見て、それが「Player」だったら
        if (col.gameObject.tag == "Player")
        {
            // ゲームオーバー用シーンを読み込んでシーン変更
            // SceneManager.LoadScene("GameOver");

            // ゲームオーバーパネルを呼び出す
            // gameoverpanel.Destroy();
            // playercontroller.playerDie();
            // Debug.Log("gameover");

        }

    }
}
