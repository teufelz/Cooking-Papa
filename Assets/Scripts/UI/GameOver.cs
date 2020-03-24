using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private GameController script;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void show(int winner)
    {
        //gameObject.SetActive(true);
        foreach (Transform child in transform)
        {
            if (child.name == "TextOver")
            {
                child.GetComponent<UnityEngine.UI.Text>().text = "Player" + winner.ToString() + " is the winner \nPress 'space' to restart game";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //int winner = script.getWinner();
        //if (winner > 0)
        //{
        //    gameObject.SetActive(true);
        //}
        //gameObject.GetComponent<UnityEngine.UI.Text>().text = "Player" + winner.ToString() + " is the winner \nPress 'space' to restart game";
    }
}

