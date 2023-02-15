using UnityEngine;

namespace ClearSky
{
    public class PlayerController : MonoBehaviour
    {

        public float movePower = 10f;
        public float jumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5

        //リジットボディ2D用変数
        private Rigidbody2D rb;
        // 当たり判定の取得用変数
        private CapsuleCollider2D capcol = null;
        [Header("踏みつけ判定の高さの割合")] public float stepOnRate;
        // 敵を踏んだ時のジャンプ
        private bool isOtherJump = false;
        private float otherJumpHeight = 0.0f;

        private Animator anim;
        private int direction = 1;
        bool isJumping = false;
        private bool alive = true;


        // ダウン処理の変数を作る
        private bool isDown = false;

        //敵のデータを変数に入れる
        private string enemyTag = "Enemy";
        private string trapTag = "Trap";
        private string clearTag = "Clear";

        [SerializeField] UIManager UIManager;


        void Start()
        {
            //このスクリプトについているコンポーネント各種の場所を取得し、格納する
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            //プレイヤーのコライダーを取得
            capcol = GetComponent<CapsuleCollider2D>();
        }

        private void Update()
        {
            if (!isDown)
            {
                Restart();
                if (alive)
                {
                    Hurt();
                    Die();
                    Attack();
                    Jump();
                    Run();

                }
            }
            else
            {
                //rb.velocity = new Vector2(0, -gravity);
            }
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            anim.SetBool("isJump", false);
        }


        // 敵を踏んだ時のジャンプ
        void OtherJump()
        {
            if (isOtherJump)
            {
                isJumping = true;
                anim.SetBool("isJump", true);

            }
            else
            {
                isOtherJump = false;
            }
            //if (!isJumping)
            //{
            //    return;
            //}

            rb.velocity = Vector2.zero;

            // 敵を踏んだ時のジャンプの高さ
            Vector2 jumpVelocity = new Vector2(0, otherJumpHeight);

        }

        void Run()
        {
            Vector3 moveVelocity = Vector3.zero;
            anim.SetBool("isRun", false);


            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                direction = -1;
                moveVelocity = Vector3.left;

                transform.localScale = new Vector3(direction, 1, 1);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);

            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                direction = 1;
                moveVelocity = Vector3.right;

                transform.localScale = new Vector3(direction, 1, 1);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);

            }
            transform.position += moveVelocity * movePower * Time.deltaTime;
        }

        void Jump()
        {
            if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0) && !anim.GetBool("isJump") && !isJumping)
            {
                isJumping = true;
                anim.SetBool("isJump", true);
            }
            if (!isJumping)
            {
                return;
            }

            rb.velocity = Vector2.zero;

            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

            isJumping = false;
        }

        void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                anim.SetTrigger("attack");
            }
        }

        void Hurt()
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                anim.SetTrigger("hurt");
                if (direction == 1)
                    rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
                else
                    rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
            }
        }

        void Die()
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                anim.SetTrigger("die");
                alive = false;
            }
        }

        void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                anim.SetTrigger("idle");
                alive = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag(enemyTag))
            {
                // 踏みつけ判定になる高さ
                float stepOnHeight = (capcol.size.y * (stepOnRate / 100f));

                // 踏みつけ判定のワールド座標
                float judgePos = transform.position.y - (capcol.size.y / 2f) + stepOnHeight;

                foreach (ContactPoint2D p in collision.contacts)
                {
                    // もう一度跳ねる
                    if (p.point.y < judgePos)
                    {
                        ObjectCollision o = collision.gameObject.GetComponent<ObjectCollision>();
                        if (o != null)
                        {
                            otherJumpHeight = o.boundHeight; //踏んづけたものから跳ねる力を取得する
                            o.playerStepOn = true; // 踏んづけたものに対して踏んづけた
                            jumpPower = transform.position.y; // ジャンプした位置を記録する
                            isOtherJump = true;
                            isJumping = false;
                            // jumpTime = 0.0f;
                        }
                        else
                        {
                            // Debug.Log("ObjectCollisionがついてないよ!");
                        }
                    }
                    else
                    {
                        // ダウンする
                        anim.Play("Die");
                        isDown = true;
                        UIManager.GameOver();
                        // gameoverpanel.SetActive(true); 
                        break;
                    }
                }
            }
            else if (collision.collider.CompareTag(trapTag))
            {
                anim.Play("Die");
                isDown=true;
                UIManager.GameOver();
                // gameoverpanel.SetActive(true);
            }
            else if (collision.collider.CompareTag(clearTag))
            {
                anim.Play("Attack");
               // anim.SetFloat("MovingSpeed", 0.0f);
                isDown =true;
                UIManager.GameClear();
            }
        }

    }
}