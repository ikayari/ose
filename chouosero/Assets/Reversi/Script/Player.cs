using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*using Unity.MLAgents;
Agent*/
public class Player : MonoBehaviour
{
    public KOMA_STATE playerState = KOMA_STATE.Black;
    public bool chose_koma = false;
    public List<Koma> Koma;
    //public bool m_pass = false;
    public GameObject m_passUI = null;
    public Bord m_bord;

    //AIの実装関係
    public List<Koma> AICanPutPlace;
    public bool AI = false;
    float AI_timer = 0.0f;
    float AI_timer_limit = 1.2f;//人がプレイするときに使う時間。
    float AIlearning_timer_limit = 0.8f;//AI抽出時に使う時間。

    //特殊ゴマ関係

    /// <summary>
    /// 2回行動ゴマ。このコマを置くともう一度自分の番。
    /// コマの発生に制限を付けないと無限に手番を行える。
    /// </summary>
    public bool TwoMoveState = false;

    /// <summary>
    /// 手助けコマ。自分の番に相手のコマをおく。
    /// </summary>
    public bool SupportMoveState = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //HumanPlayer();
        RandomPlayer();
        /*if (AI)
        {
            RandomPlayer();
        }
        else
        {
            HumanPlayer();
        }*/
    }

    private void AIPlayer()
    {
        if (AI == false)
        {
            //人がプレイする手番なったらプレイ出来ないように。
            return;
        }

        Koma.Clear();//初期化。

        //遅延を入れないと複数選んでしまう為。
        AI_timer += Time.deltaTime;

        if (AI_timer <= AIlearning_timer_limit)
        {
            return;
        }
        //かつ初期化されていれば。
        if (!m_bord.m_Initialize)
        {
            return;
        }

        //置ける場所が1つ以上あれば。
        if (AICanPutPlace.Count >= 1)
        {
            //コマを置ける場所を最大数としたListの中でランダムに場所を選ぶ。
            var randomIndex = UnityEngine.Random.Range(0, AICanPutPlace.Count);

            if (Koma.Count == 0)
            {
                //ランダムに選んだ場所にコマを追加。
                Koma.Add(AICanPutPlace[randomIndex]);
                chose_koma = true;
                AI = false;

                AI_timer = 0.0f;
            }
        }
        else
        {
            PlayerStateChange();
            AI = false;

            AI_timer = 0.0f;
        }

    }

    public void EndGame()
    {
        //報酬を与える。
        //AddReward(1.0f);
        //学習1周終わる。
        //EndEpisode();
    }

    private void RandomPlayer()
    {
        /*if (AI == false)
        {
            //人がプレイする手番になったらプレイ出来ないように。
            return;
        }*/
        //かつ初期化されてなかったらreturn
        if (m_bord.m_Initialize)
        {
            return;
        }

        if (TwoMoveState == true)
        {
            Koma.Clear();
            TwoMoveState = false;
            AI = false;
            PlayerStateChange();
            return;
        }

        Koma.Clear();//初期化。

        //遅延を入れないと複数選んでしまう為。
        AI_timer += Time.deltaTime;

        if (AI_timer <= AIlearning_timer_limit)
        {
            return;
        }


        //置ける場所が1つ以上あれば。
        if (AICanPutPlace.Count >= 1)
        {
            //コマを置ける場所を最大数としたListの中でランダムに場所を選ぶ。
            var randomIndex = UnityEngine.Random.Range(0, AICanPutPlace.Count);

            if (Koma.Count == 0)
            {
                //ランダムに選んだ場所にコマを追加。
                Koma.Add(AICanPutPlace[randomIndex]);
                chose_koma = true;
                AI = false;

                AI_timer = 0.0f;
            }
        }
        else
        {
            PlayerStateChange();
            AI = false;

            AI_timer = 0.0f;
        }

    }

    private void HumanPlayer()
    {
        /*if (AI == true)
        {
            //AIがプレイする手番になったらプレイ出来ないように。
            return;
        }*/

        //2回行動
        if (Input.GetKeyDown(KeyCode.A))
        {
            TwoMoveState = true;
        }


        //手助け
        if (Input.GetKeyDown(KeyCode.S))
        {
            SupportMoveState = true;
            PlayerStateChange();
        }


        if (Input.GetMouseButtonDown(0))
        {
            Koma.Clear();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Chose")) ;
                {
                    if (hit.collider.gameObject.transform.parent.gameObject.GetComponent<Koma>().GetState() == KOMA_STATE.Empty)
                    {
                        Koma.Add(hit.collider.gameObject.transform.parent.gameObject.GetComponent<Koma>());
                        chose_koma = true;
                        m_bord.m_addnum = false;
                        if (TwoMoveState == false)
                        {
                            AI = true;
                        }
                    }
                }
            }
        }
    }

    public void PlayerStateChange()
    {

        if (playerState == KOMA_STATE.Empty)
        {
            Debug.Log("プレイヤーのコマステートがEmpty");
            return;
        }
        if (playerState == KOMA_STATE.Black)
        {
            playerState = KOMA_STATE.White;
        }
        else
        {
            playerState = KOMA_STATE.Black;
        }

    }

    public void PlayerPass()
    {
        m_passUI.GetComponent<Animator>().SetTrigger("PassAnim");

        PlayerStateChange();
        m_bord.CanChange = false;
        m_bord.m_addnum = false;
    }
}
