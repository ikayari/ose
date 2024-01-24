using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeInOut : MonoBehaviour
{
    public GameObject m_fadeObjectParent;
    public Image m_fadeObject;
    public bool m_Startfadein = true;//�V�[���ǂݍ��ݎ��t�F�[�h�C���H
    public bool m_isfade = false;//�t�F�[�h���H
    public bool m_isfadeout = false;//�t�F�[�h�A�E�g���H
    public bool m_isfadein = false;//�t�F�[�h�C�����H
    public float m_fadetime = 1.0f;
    public float m_roopcount = 50;
    // Start is called before the first frame update
    void Start()
    {
        m_fadeObjectParent.SetActive(true);
        if(m_Startfadein)
        {
            StartCoroutine("Color_FadeIn");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Color_FadeIn()
    {
        if (!m_isfadeout)//�t�F�[�h�A�E�g���łȂ����
        {
            // ��ʂ��t�F�[�h�C��������R�[���`��
            // �O��F��ʂ𕢂�Panel�ɃA�^�b�`���Ă���
            m_isfade = true;
            m_isfadein = true;
            // �F��ς���Q�[���I�u�W�F�N�g����Image�R���|�[�l���g���擾


            // �t�F�[�h���̐F��ݒ�i���j���ύX��
            m_fadeObject.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (255.0f / 255.0f));



            // �E�F�C�g���ԎZ�o
            float wait_time = m_fadetime / m_roopcount;

            // �F�̊Ԋu���Z�o
            float alpha_interval = 255.0f / m_roopcount;

            // �F�����X�ɕς��郋�[�v
            for (float alpha = 255.0f; alpha >= 0.0f; alpha -= alpha_interval)
            {
                // �҂�����
                yield return new WaitForSeconds(wait_time);

                // Alpha�l��������������
                Color new_color = m_fadeObject.color;
                new_color.a = alpha / 255.0f;
                m_fadeObject.color = new_color;
            }
            m_isfade = false;
            m_isfadein = false;
        }
    }
    public IEnumerator Color_FadeOut()
    {
        if (!m_isfadein)//�t�F�[�h�C�����łȂ����
        {
            // ��ʂ��t�F�[�h�C��������R�[���`��
            // �O��F��ʂ𕢂�Panel�ɃA�^�b�`���Ă���


            m_isfade = true;
            m_isfadeout = true;
            // �t�F�[�h��̐F��ݒ�i���j���ύX��
            m_fadeObject.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (0.0f / 255.0f));



            // �E�F�C�g���ԎZ�o
            float wait_time = m_fadetime / m_roopcount;

            // �F�̊Ԋu���Z�o
            float alpha_interval = 255.0f / m_roopcount;

            // �F�����X�ɕς��郋�[�v
            for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)
            {
                // �҂�����
                yield return new WaitForSeconds(wait_time);

                // Alpha�l���������グ��
                Color new_color = m_fadeObject.color;
                new_color.a = alpha / 255.0f;
                m_fadeObject.color = new_color;
            }
            m_isfade = false;
            m_isfadeout = false;
        }
    }
}
