using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class komapieces : MonoBehaviour
{
    public Bord bord;
    public int black_koma_pieces;
    public int white_koma_pieces;
    [SerializeField] private TextMeshProUGUI Black;
    [SerializeField] private TextMeshProUGUI White;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Black.text = "Black:" + black_koma_pieces.ToString("D2");
        White.text = "White:" + white_koma_pieces.ToString("D2");
    }
    public void UpdateUI()
    {

    }
    public void Resetnum()
    {
        black_koma_pieces = 0;
        white_koma_pieces = 0;
    }

}
