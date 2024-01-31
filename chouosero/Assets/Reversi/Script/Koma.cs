using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KOMA_STATE
{
    Empty = 0,
    Black = 1,
    White = 2,
}
public class Koma : MonoBehaviour
{
    Animator animator;
    public List<Koma> komalist;
    public Player player;
    private int[,] koma_Position;
    public bool koma_Initialize = false;
    public KOMA_STATE koma_State = KOMA_STATE.Empty; //"E" EmptyÇÃEÅ@ë∂ç›ÇµÇ»Ç¢ "W"White îí "B"Black çï
    public bool koma_Change = false;//trueÇÃéûÇﬂÇ≠ÇÈ
    public bool can_Flip = false;
    public GameObject can_flipObject;
    public Bord bord;
    public KOMA_STATE InitialState = KOMA_STATE.Empty;
    public bool RandomRespawn = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PutKoma(KOMA_STATE state)
    {
        SetKomaState(state);
        koma_Initialize = true;
    }
    void KomaDelete()
    {
        animator.enabled = true;
        animator.Play("DelKoma");
        //if (InitialState == KOMA_STATE.Empty)
        //{
        //    SetKomaStateEmpty();

        //}
        //else
        //{
        //    SetKomaState(InitialState);
        //    koma_Initialize = false;
        //}

        //koma_Initialize = false;
        if (RandomRespawn)
        {
            System.Random rnd = new System.Random();
            int i = rnd.Next(1, 3);
            if (i == 1)
            {
                SetKomaState(KOMA_STATE.Black);
            }
            if (i == 2)
            {
                SetKomaState(KOMA_STATE.White);
            }
            RandomRespawn = false;
        }
        koma_Initialize = true;
    }
    private void SetKomaPosition(int[,] komapos)
    {
        koma_Position = komapos;
    }
    private void SetKomaState(KOMA_STATE state)
    {
        koma_State = state;
    }
    private void SetKomaStateEmpty()
    {
        koma_State = KOMA_STATE.Empty;
    }
    private void ChangeKomaState()
    {
        if (koma_State == KOMA_STATE.Black)
        {
            koma_State = KOMA_STATE.White;

        }
        else
        {
            koma_State = KOMA_STATE.Black;
        }
        koma_Change = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!IsCanChange())
        {
            bord.CanChange = false;
        }
        animator.SetBool("Initialize", koma_Initialize);
        animator.SetInteger("State", (int)koma_State);
        animator.SetBool("Change", koma_Change);
        KomaChange();
        if (GetComponent<Animator>().enabled == false)
        {
            Invoke("KomaDelete", 1.0f);
        }
        can_flipObject.SetActive(can_Flip);


    }
    public KOMA_STATE GetState()
    {
        return koma_State;
    }
    public void SetFlipCan(bool bl)
    {
        can_Flip = bl;
    }
    bool IsCanChange()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("WhiteIdle") || animator.GetCurrentAnimatorStateInfo(0).IsName("BlackIdle") || animator.GetCurrentAnimatorStateInfo(0).IsName("New State"))
        {
            return true;
        }
        return false;
        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("ChangeWhite") || animator.GetCurrentAnimatorStateInfo(0).IsName("ChangeBlack") || animator.GetCurrentAnimatorStateInfo(0).IsName("SpawnBlack") || animator.GetCurrentAnimatorStateInfo(0).IsName("SpawnWhite"))
        //{
        //    return false;
        //}
        //return true;
    }
    private void KomaChange()
    {
        if (koma_Change)
        {


            if (!IsCanChange())
            {
                return;
            }

            if (koma_State == KOMA_STATE.Black)
            {
                koma_State = KOMA_STATE.White;

            }
            else
            {
                koma_State = KOMA_STATE.Black;
            }
            koma_Change = !koma_Change;
        }
    }
    public void KomaReversi()
    {
        koma_Change = true;
    }
}
