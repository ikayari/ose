using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightTargetMouthPos : MonoBehaviour
{
    public float zpos = 10.0f;
    public GameObject m_target = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �J�[�\���ʒu���擾
        Vector3 mousePosition = Input.mousePosition;
        // �J�[�\���ʒu��z���W��10��
        mousePosition.z = zpos;
        // �J�[�\���ʒu�����[���h���W�ɕϊ�
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);

        m_target.transform.position = target;
        Quaternion rot=Quaternion.identity;
        Vector3 directionToTarget = m_target.transform.position - transform.position;
        rot.SetLookRotation(directionToTarget);

        
        transform.rotation = rot;
        
    }
}
