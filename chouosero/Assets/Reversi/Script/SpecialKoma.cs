using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialKoma : MonoBehaviour
{
    public Player m_player;

    public int BordPutKomaCount = 4;
    /// <summary>
    /// お互いに3ターン経過したら、特殊ゴマを使えるように。
    /// </summary>
    private int SpecialKomaStart = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void SelectKoma()
    {
        if (BordPutKomaCount < SpecialKomaStart)
        {
            m_player.OnceAction = true;
            return;
        }

        int randomNum = UnityEngine.Random.Range(0, 100);

        switch (RandomChooseKoma(randomNum))
        {
            case 1:
                m_player.TwoMoveStateTrue();
                break;
            case 2:
                m_player.SupportMoveStateTrue();
                break;
            default:
                break;
        }
        m_player.OnceAction = true;
    }

    private int RandomChooseKoma(int num)
    {
        if (num < 15)
        {
            return 1;
        }
        else if (num < 30)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    public void ResetBordPutKomaCount()
    {
        BordPutKomaCount = 0;
    }
}
