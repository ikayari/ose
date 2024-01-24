using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeInOut : MonoBehaviour
{
    public GameObject m_fadeObjectParent;
    public Image m_fadeObject;
    public bool m_Startfadein = true;//シーン読み込み時フェードイン？
    public bool m_isfade = false;//フェード中？
    public bool m_isfadeout = false;//フェードアウト中？
    public bool m_isfadein = false;//フェードイン中？
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
        if (!m_isfadeout)//フェードアウト中でなければ
        {
            // 画面をフェードインさせるコールチン
            // 前提：画面を覆うPanelにアタッチしている
            m_isfade = true;
            m_isfadein = true;
            // 色を変えるゲームオブジェクトからImageコンポーネントを取得


            // フェード元の色を設定（黒）★変更可
            m_fadeObject.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (255.0f / 255.0f));



            // ウェイト時間算出
            float wait_time = m_fadetime / m_roopcount;

            // 色の間隔を算出
            float alpha_interval = 255.0f / m_roopcount;

            // 色を徐々に変えるループ
            for (float alpha = 255.0f; alpha >= 0.0f; alpha -= alpha_interval)
            {
                // 待ち時間
                yield return new WaitForSeconds(wait_time);

                // Alpha値を少しずつ下げる
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
        if (!m_isfadein)//フェードイン中でなければ
        {
            // 画面をフェードインさせるコールチン
            // 前提：画面を覆うPanelにアタッチしている


            m_isfade = true;
            m_isfadeout = true;
            // フェード後の色を設定（黒）★変更可
            m_fadeObject.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (0.0f / 255.0f));



            // ウェイト時間算出
            float wait_time = m_fadetime / m_roopcount;

            // 色の間隔を算出
            float alpha_interval = 255.0f / m_roopcount;

            // 色を徐々に変えるループ
            for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)
            {
                // 待ち時間
                yield return new WaitForSeconds(wait_time);

                // Alpha値を少しずつ上げる
                Color new_color = m_fadeObject.color;
                new_color.a = alpha / 255.0f;
                m_fadeObject.color = new_color;
            }
            m_isfade = false;
            m_isfadeout = false;
        }
    }
}
