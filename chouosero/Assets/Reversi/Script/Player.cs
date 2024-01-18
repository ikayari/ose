using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public KOMA_STATE playerState = KOMA_STATE.Black;
    public bool chose_koma = false;
    public List<Koma> Koma;
    public bool m_pass = false;
    public GameObject m_passUI = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Koma.Clear();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Chose"))
                {
                    if (hit.collider.gameObject.transform.parent.gameObject.GetComponent<Koma>().GetState() == KOMA_STATE.Empty)
                    {
                        Koma.Add(hit.collider.gameObject.transform.parent.gameObject.GetComponent<Koma>());
                        chose_koma = true;
                    }
                }
            }
        }
    }
    public void PlayerStateChange()
    {
        
        if(playerState==KOMA_STATE.Empty)
        {
            Debug.Log("プレイヤーのコマステートがEmpty");
            return;
        }
        if(playerState==KOMA_STATE.Black)
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
    }
}
