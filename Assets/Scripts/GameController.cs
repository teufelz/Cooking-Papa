using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private int turn;
    private string phase;
    private int player;


    // Start is called before the first frame update
    void Start()
    {
        turn = 1;
        player = turn;
        phase = "StartTurn";
        StartCoroutine(Controller());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Controller()
    {
        while (true)
        {
            switch (phase)
            {
                case "StartTurn":
                    // check effect then proceed to next phase
                    //wait
                    yield return StartCoroutine(StartPhase());

                    break;

                case "DrawIngredient":
                    // check number of card 
                    // draw two choose 1
                    yield return StartCoroutine(DrawPhase());
                    break;

                case "DrawEvent":
                    // draw event
                    // check event type proceed to next phase (Instant or Usable) 
                    break;

                case "InstantEvent":

                    //if have usable on hand proceed to usable
                    break;

                case "UsableEvent":
                    // use or skip
                    break;

                case "Cooking":

                    break;

                case "EndTurn":
                    //check dish & hand & score
                    turn += 1;
                    player = turn % 4;
                    break;
            }
        }
    }
    IEnumerator StartPhase()
    {
        Debug.Log("Start Phase");
        yield return StartCoroutine(WaitForSelection());
        phase = "DrawIngredient";
    }

    IEnumerator WaitForSelection()
    {
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
    }
    IEnumerator DrawPhase()
    {
        Debug.Log("Draw Phase");
        yield return StartCoroutine(WaitForSelection());
        phase = "StartTurn";
    }
}
