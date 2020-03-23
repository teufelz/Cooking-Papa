using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private GameController script;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameController = GameObject.Find("GameController");
        script = gameController.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        string scoreText = "Player1: "+script.scores[0].ToString()+"\nPlayer2: "+script.scores[1].ToString()+"\nPlayer3: "+script.scores[2].ToString()+"\nPlayer4: "+script.scores[3].ToString();
        gameObject.GetComponent<UnityEngine.UI.Text>().text = scoreText;
    }
}
