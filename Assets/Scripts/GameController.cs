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
                    yield return StartCoroutine(StartPhase());
                    break;

                case "DrawIngredient":
                    yield return StartCoroutine(DrawIngredientPhase());
                    break;

                case "DrawEvent":
                    yield return StartCoroutine(DrawEventPhase());
                    break;

                case "InstantEvent":
                    yield return StartCoroutine(InstantEventPhase());
                    break;

                case "UsableEvent":
                    yield return StartCoroutine(UsableEventPhase());
                    break;

                case "Cooking":
                    yield return StartCoroutine(CookingPhase());
                    break;

                case "EndTurn":
                    yield return StartCoroutine(EndTurnPhase());
                    break;
            }
        }
    }
    IEnumerator StartPhase()
    {
        Debug.Log("Start Phase Turn : "+turn);
        // check effect then proceed to next phase
        yield return new WaitForSeconds(1);
        phase = "DrawIngredient";
    }

    IEnumerator DrawIngredientPhase()
    {
        Debug.Log("Draw Ingredient Phase");
        yield return new WaitForSeconds(1);
        // check number of card 
        // draw two choose 1
        yield return StartCoroutine(WaitForDrawCard());
        phase = "DrawEvent";
    }

    IEnumerator WaitForDrawCard()
    {
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
    }

    IEnumerator DrawEventPhase()
    {
        Debug.Log("Draw Event Phase");
        yield return new WaitForSeconds(1);
        // draw event
        // check event type proceed to next phase (Instant or Usable) 
        //if (event card type) 
        phase = "InstantEvent";
        //else if have usable on hand proceed to usable
        //phase = UsableEvent 
        //else 
        // phase = Cooking
    }

    IEnumerator InstantEventPhase()
    {
        Debug.Log("Instant Phase");
        yield return new WaitForSeconds(1);
        //if have usable on hand proceed to usable
        phase = "UsableEvent";
    }

    IEnumerator UsableEventPhase()
    {
        Debug.Log("Usable Phase");
        yield return new WaitForSeconds(1);
        // use or skip
        yield return StartCoroutine(WaitForUsable());
        phase = "Cooking";
    }

    IEnumerator WaitForUsable()
    {
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
    }

    IEnumerator CookingPhase()
    {
        Debug.Log("Cooking Phase");
        yield return new WaitForSeconds(1);
        // cook or skip
        yield return StartCoroutine(WaitForCooking());
        phase = "EndTurn";
    }

    IEnumerator WaitForCooking()
    {
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
    }

    IEnumerator EndTurnPhase()
    {
        Debug.Log("End Phase");
        yield return new WaitForSeconds(1);
        //check dish & hand & score
        turn += 1;
        player = turn % 4 + 1;
        phase = "StartTurn";
    }
}
