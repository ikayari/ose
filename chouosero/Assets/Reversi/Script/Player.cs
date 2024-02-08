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

    //AI�̎����֌W
    public List<Koma> AICanPutPlace;
    public bool AI = false;
    float AI_timer = 0.0f;
    float AI_timer_limit = 1.2f;//�l���v���C����Ƃ��Ɏg�����ԁB
    float AIlearning_timer_limit = 0.8f;//AI���o���Ɏg�����ԁB

    //����S�}�֌W

    /// <summary>
    /// 2��s���S�}�B���̃R�}��u���Ƃ�����x�����̔ԁB
    /// �R�}�̔����ɐ�����t���Ȃ��Ɩ����Ɏ�Ԃ��s����B
    /// </summary>
    public bool TwoMoveState = false;

    /// <summary>
    /// �菕���R�}�B�����̔Ԃɑ���̃R�}�������B
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
            //�l���v���C�����ԂȂ�����v���C�o���Ȃ��悤�ɁB
            return;
        }

        Koma.Clear();//�������B

        //�x�������Ȃ��ƕ����I��ł��܂��ׁB
        AI_timer += Time.deltaTime;

        if (AI_timer <= AIlearning_timer_limit)
        {
            return;
        }
        //������������Ă���΁B
        if (!m_bord.m_Initialize)
        {
            return;
        }

        //�u����ꏊ��1�ȏ゠��΁B
        if (AICanPutPlace.Count >= 1)
        {
            //�R�}��u����ꏊ���ő吔�Ƃ���List�̒��Ń����_���ɏꏊ��I�ԁB
            var randomIndex = UnityEngine.Random.Range(0, AICanPutPlace.Count);

            if (Koma.Count == 0)
            {
                //�����_���ɑI�񂾏ꏊ�ɃR�}��ǉ��B
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
        //��V��^����B
        //AddReward(1.0f);
        //�w�K1���I���B
        //EndEpisode();
    }

    private void RandomPlayer()
    {
        /*if (AI == false)
        {
            //�l���v���C�����ԂɂȂ�����v���C�o���Ȃ��悤�ɁB
            return;
        }*/
        //������������ĂȂ�������return
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

        Koma.Clear();//�������B

        //�x�������Ȃ��ƕ����I��ł��܂��ׁB
        AI_timer += Time.deltaTime;

        if (AI_timer <= AIlearning_timer_limit)
        {
            return;
        }


        //�u����ꏊ��1�ȏ゠��΁B
        if (AICanPutPlace.Count >= 1)
        {
            //�R�}��u����ꏊ���ő吔�Ƃ���List�̒��Ń����_���ɏꏊ��I�ԁB
            var randomIndex = UnityEngine.Random.Range(0, AICanPutPlace.Count);

            if (Koma.Count == 0)
            {
                //�����_���ɑI�񂾏ꏊ�ɃR�}��ǉ��B
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
            //AI���v���C�����ԂɂȂ�����v���C�o���Ȃ��悤�ɁB
            return;
        }*/

        //2��s��
        if (Input.GetKeyDown(KeyCode.A))
        {
            TwoMoveState = true;
        }


        //�菕��
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
            Debug.Log("�v���C���[�̃R�}�X�e�[�g��Empty");
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
