using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bord : MonoBehaviour
{
    public enum DILECTION
    {
        UP,
        RIGHT,
        DOWN,
        LEFT,
        UPRIGHT,
        UPLEFT,
        DOWNRIGHT,
        DOWNLEFT
    }
    public GameObject komaRoot;

    public List<Koma> komalist;
    public Koma [,] komas = new Koma[8, 8];
    public PassButton passbutton;
    private int[,] bord = new int[8, 8];
    public Player player1;
    float reversi_timer = 0.0f;
    float reversi_timer_limit = 10.5f;
    bool end = false;
    public bool CanChange = false;
    public bool m_Initialize = false;

    public komapieces KomaPiecesUI;


    // Start is called before the first frame update
    void Start()
    {
        int j = 0;
        int k = 0;
        for (int i=0;i<komalist.Count;i++)
        {

            komas[j,k] = komalist[i];
            k++;
            if(k>7)
            {
                j++;
                k = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.m_passUI.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("PAss"))
        {
            CanChange = false;
        }
        UpdateBord();        

      
    }

    void UpdateBord()
    {

        FlipCheckReset();
        if (CanChange)
        {
            FlipCheck();
        }
        CanChange = true;
        if (player1.chose_koma)
        {
            CanChange = false;
            player1.Koma[0].PutKoma(player1.playerState);
            reversi_timer = 0.0f;
            for (int i = 0; i < player1.Koma[0].komalist.Count;)
            {
                if (reversi_timer == 0.0f)
                {
                    player1.Koma[0].komalist[i].KomaReversi();
                    i++;
                    reversi_timer += Time.deltaTime;
                }
                else if(reversi_timer <= reversi_timer_limit)
                {
                    reversi_timer += Time.deltaTime;
                    
                    continue;
                }
                else if(reversi_timer >= reversi_timer_limit)
                {
                    reversi_timer = 0.0f;
                    
                    continue;
                }
                end = true;
            }
            if(end)
            {
                end = false;
                player1.PlayerStateChange();
                player1.Koma.Clear();
                player1.chose_koma = false;
                CanChange = false;
            }
        }
    }
    public void FlipCheck()//マジですまないが、この中にkomapiecesの更新処理が入っている。
    {
        KomaPiecesUI.Resetnum();
        passbutton.canReversKoma = false;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (komas[i, j].GetState() == KOMA_STATE.Black)
                {
                    KomaPiecesUI.black_koma_pieces++;
                }
                else if (komas[i, j].GetState() == KOMA_STATE.White)
                {
                    KomaPiecesUI.white_koma_pieces++;
                }
                if (komas[i, j].GetState() != KOMA_STATE.Empty)
                {
                   
                }
                else
                {
                    bool left = j - 1 > 0;
                    bool up = i - 1 > 0;
                    bool right = j + 1 < 7;
                    bool down = i + 1 < 7;

                    if (up)
                    {
                        // UPCheck
                        CheckFlipKoma(i, j, DILECTION.UP, player1.playerState);
                    }
                    if (right)
                    {
                        // RightCheck
                        CheckFlipKoma(i, j, DILECTION.RIGHT, player1.playerState);
                    }
                    if (down)
                    {
                        // DownCheck
                        CheckFlipKoma(i, j, DILECTION.DOWN, player1.playerState);
                    }
                    if (left)
                    {
                        // LeftCheck
                        CheckFlipKoma(i, j, DILECTION.LEFT, player1.playerState);
                    }
                    if (up && right)
                    {
                        // UPRightCheck
                        CheckFlipKoma(i, j, DILECTION.UPRIGHT, player1.playerState);
                    }
                    if (up && left)
                    {
                        // UPLeftCheck
                        CheckFlipKoma(i, j, DILECTION.UPLEFT, player1.playerState);
                    }
                    if (down && right)
                    {
                        // DownRightCheck
                        CheckFlipKoma(i, j, DILECTION.DOWNRIGHT, player1.playerState);
                    }
                    if (down && left)
                    {
                        // DownLeftCheck
                        CheckFlipKoma(i, j, DILECTION.DOWNLEFT, player1.playerState);
                    }


                }
            }
        }
    }
    void FlipCheckReset()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                komas[i, j].komalist.Clear();
                komas[i, j].SetFlipCan(false); //めくれない状態に設定                
            }
        }
    }
    void InitializeBord()
    {

    }
     void GetChildren(GameObject obj)
    {
        Transform children = obj.GetComponentInChildren<Transform>();
        //子要素がいなければ終了
        if (children.childCount == 0)
        {
            return;
        }
        foreach (Transform ob in children)
        {
            GetChildren(ob.gameObject);
        }
    }
    void CheckFlipKoma(int x, int y, DILECTION direction, KOMA_STATE playerstate)
    {
        if (playerstate == KOMA_STATE.Empty)//判別不可
        {
            Debug.Log("PlayerState is Empty. CheckFlipKoma()");
            return;
        }
        int dirx = 0;
        int diry = 0;
        switch (direction)
        {
            case DILECTION.UP:
                dirx = -1;
                break;
            case DILECTION.RIGHT:
                diry = 1;
                break;
            case DILECTION.LEFT:
                diry = -1;
                break;
            case DILECTION.DOWN:
                dirx = 1;
                break;
            case DILECTION.UPRIGHT:
                diry = 1;
                dirx = -1;
                break;
            case DILECTION.UPLEFT:
                diry = -1;
                dirx = -1;
                break;
            case DILECTION.DOWNLEFT:
                diry = -1;
                dirx = 1;
                break;
            case DILECTION.DOWNRIGHT:
                diry = 1;
                dirx = 1;
                break;
        }
        int i = 1;
        while (true)
        {

            if (x + (dirx * i) < 0 || x + (dirx * i) > 7 || y + (diry * i) < 0 || y + (diry * i) > 7)
            {
                //範囲外に出ちゃってる…！
                //中止…

                break;
            }
            if (komas[x + (dirx * i), y + (diry * i)].GetState() == playerstate)//同じ色の時
            {
                if (i == 1)//隣が同じ色だよねこれ？？
                {
                    //じゃあむり
                    break;
                }
                else//色々あって同じ色を発見した！！！
                {
                    //つまるところめくれるはず！
                    komas[x, y].SetFlipCan(true);

                    //めくれる部分登録する！
                    for (int k = 1; k < i; k++)
                    {
                        komas[x, y].komalist.Add(komas[x + (dirx * k), y + (diry * k)]);
                        passbutton.canReversKoma = true;
                    }

                    break;
                }
            }
            else if (komas[x + (dirx * i), y + (diry * i)].GetState() == KOMA_STATE.Empty)//これ以上コマないわ。
            {
                //むり
                break;
            }
            else //コマが置いていてプレイヤーステートと反対の色の時
            {
                //いけるかも。（継続）
            }
            i++;

        }

    }

}
