using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public Bord m_bord;
    public SceneChange m_sceneChange;
    bool m_result = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_bord.endGame&&!m_result)
        {
            EndGameResult();
            m_result = true;
        }
    }
    public void EndGameResult()
    {
        if(m_bord.KomaPiecesUI.black_koma_pieces>m_bord.KomaPiecesUI.white_koma_pieces)
        {
            //•‚ÌŸ‚¿
            Debug.Log("•‚ÌŸ‚¿");
            m_sceneChange.StartSceneChange();
        }
        else if(m_bord.KomaPiecesUI.black_koma_pieces<m_bord.KomaPiecesUI.white_koma_pieces)
        {
            //”’‚ÌŸ‚¿
            Debug.Log("”’‚ÌŸ‚¿");
            m_sceneChange.StartSceneChange();
        }
        else if(m_bord.KomaPiecesUI.black_koma_pieces == m_bord.KomaPiecesUI.white_koma_pieces)
        {
            //ˆø‚«•ª‚¯
            Debug.Log("ˆø‚«•ª‚¯I");
            m_sceneChange.StartSceneChange();
        }
        
    }
}
