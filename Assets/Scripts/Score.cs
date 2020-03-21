using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public static int scoreValue = 0;
    public Text ScoreText;
    void Start(){
        ScoreText = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "SCORE:" + scoreValue;
    }
}