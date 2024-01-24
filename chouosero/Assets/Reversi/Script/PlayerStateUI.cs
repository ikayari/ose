using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStateUI : MonoBehaviour
{
    public Player m_player;
    public GameObject StateBlack;
    public GameObject StateWhite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_player.playerState==KOMA_STATE.Black)
        {
            StateBlack.SetActive(true);
            StateWhite.SetActive(false);
        }
        else if(m_player.playerState==KOMA_STATE.White)
        {
            StateBlack.SetActive(false);
            StateWhite.SetActive(true);
        }
    }
}
