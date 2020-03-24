using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerText : MonoBehaviour
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
        string player = script.player.ToString();
        string phase = script.getPhase();
        gameObject.GetComponent<UnityEngine.UI.Text>().text = "Player " + player + "\nPhase: " + phase;
    }
}
