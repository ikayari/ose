using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // シーンを扱う場合これを追加しないとダメ

public class SceneChange : MonoBehaviour
{
    public FadeInOut m_fadeObject;
    public string SceneName;    // 遷移先のシーン名
    public bool m_sceneChange = false;

    void Update()
    {
        //// スペースキーが押されたらシーンを切り替える
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartCoroutine("Color_FadeOut");
        //    m_sceneChange = true;
        //}
        if (m_sceneChange && !m_fadeObject.m_isfade)
        {
            SceneManager.LoadScene(SceneName);
        }
    }
    public void StartSceneChange()
    {
        m_fadeObject.StartCoroutine("Color_FadeOut");
        m_sceneChange = true;
        m_fadeObject.m_isfade = true;
    }
}