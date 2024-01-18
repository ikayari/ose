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
        // カーソル位置を取得
        Vector3 mousePosition = Input.mousePosition;
        // カーソル位置のz座標を10に
        mousePosition.z = zpos;
        // カーソル位置をワールド座標に変換
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);

        m_target.transform.position = target;
        Quaternion rot=Quaternion.identity;
        Vector3 directionToTarget = m_target.transform.position - transform.position;
        rot.SetLookRotation(directionToTarget);

        
        transform.rotation = rot;
        
    }
}
