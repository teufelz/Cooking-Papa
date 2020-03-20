using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject prefabIng;

    public List<Transform> AllIngTransform;
    public Transform dishTransform;

    public Transform overlayCanvas;

    private int turn;
    private string phase;
    private int player;

    private List<IngredientCard> player1Ing;
    private List<IngredientCard> player2Ing;
    private List<IngredientCard> player3Ing;
    private List<IngredientCard> player4Ing;

    private List<IngredientCard> ingredientDeck;
    private List<EventCard> eventDeck;
    private List<DishCard> dishDeck;

    List<List<IngredientCard>> AllIngCard;

    private int ingredientIdx;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        StartCoroutine(Controller());
    }

    private void Initialize()
    {
        turn = 1;
        player = turn % 4;
        phase = "StartTurn";

        player1Ing = new List<IngredientCard>();
        player2Ing = new List<IngredientCard>();
        player3Ing = new List<IngredientCard>();
        player4Ing = new List<IngredientCard>();

        AllIngCard = new List<List<IngredientCard>>()
        {
            player1Ing,
            player2Ing,
            player3Ing,
            player4Ing
        };

        GenerateIngDeck();
        InitializeDraw();
    }

    private void GenerateIngDeck()
    {
        ingredientDeck = new List<IngredientCard>();

        Dictionary<string, bool> paths = new Dictionary<string, bool>();
        paths["Cards/Ingredient/MeatBonus"] = true;
        paths["Cards/Ingredient/FishBonus"] = true;
        paths["Cards/Ingredient/FlourBonus"] = true;
        paths["Cards/Ingredient/SpinachBonus"] = true;
        paths["Cards/Ingredient/Meat"] = false;
        paths["Cards/Ingredient/Fish"] = false;
        paths["Cards/Ingredient/Flour"] = false;
        paths["Cards/Ingredient/Spinach"] = false;

        //load cards
        foreach (string path in paths.Keys)
        {
            if (paths[path])
            {
                for (int i = 0; i < 2; i++) 
                    ingredientDeck.Add(Resources.Load(path) as IngredientCard);
            }
            else
            {
                for (int i = 0; i < 8; i++)
                    ingredientDeck.Add(Resources.Load(path) as IngredientCard);
            }
        }

        //shuffle cards
        for (int i = 0; i < ingredientDeck.Count; i++)
        {
            IngredientCard temp = ingredientDeck[i];
            int randomIndex = Random.Range(i, ingredientDeck.Count);
            ingredientDeck[i] = ingredientDeck[randomIndex];
            ingredientDeck[randomIndex] = temp;
        }

        //set index to 0
        ingredientIdx = 0;
    }

    private void InitializeDraw()
    {
        for (int idx = 0; idx < 4; idx++)
        {
            for (int count = 0; count< 5; count++)
            {
                AllIngCard[idx].Add(ingredientDeck[ingredientIdx]);
                ingredientIdx = (ingredientIdx + 1) % ingredientDeck.Count;
            }
            UpdateIngCard(idx, idx);
        }
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
        while (AllIngCard[player - 1].Count < 2)
        {
            AllIngCard[player - 1].Add(ingredientDeck[ingredientIdx]);
            ingredientIdx = (ingredientIdx + 1) % ingredientDeck.Count;
            UpdateIngCard(0, player - 1);
        }
        
        // draw two choose 1
        IngredientCard choice1 = ingredientDeck[ingredientIdx];
        IngredientCard choice2 = ingredientDeck[ingredientIdx + 1];

        ingredientIdx = (ingredientIdx + 2) % ingredientDeck.Count;

        IngredientCard selected = null;

        yield return StartCoroutine(WaitForDrawCard(choice1, choice2, value => selected = value));

        AllIngCard[player - 1].Add(selected);
        UpdateIngCard(0, player - 1);

        phase = "DrawEvent";
    }

    private void UpdateIngCard(int transfromIdx, int playerIdx)
    {
        int noCardDiff = AllIngTransform[transfromIdx].childCount - AllIngCard[playerIdx].Count;

        if (noCardDiff > 0)
        {
            for (int count = 0; count < noCardDiff; count++)
            {
                Destroy(AllIngTransform[transfromIdx].GetChild(AllIngTransform[transfromIdx].childCount - count - 1).gameObject);
            }
        }
        else if (noCardDiff < 0)
        {
            for (int count = 0; count > noCardDiff; count--)
            {
                Instantiate(prefabIng, AllIngTransform[transfromIdx]).GetComponent<CardBehavior>().canvas = overlayCanvas;
            }
        }

        for (int idx = 0; idx < AllIngCard[playerIdx].Count; idx++)
        {
            IngredientCardViz Viz = AllIngTransform[transfromIdx].GetChild(idx).GetComponent<IngredientCardViz>();
            Viz.LoadCard(AllIngCard[playerIdx][idx]);
        }
    }

    IEnumerator WaitForDrawCard(IngredientCard choice1, IngredientCard choice2 , System.Action<IngredientCard> result)
    {
        bool selected = false;
        Debug.Log(choice1);
        Debug.Log(choice2);

        while (!selected)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                selected = true;
                result(choice1);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                selected = true;
                result(choice2);
            }
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
        CyclePlayerIng();
        CyclePlayerHand();
        player = turn % 4 + 1;
        turn += 1;
        phase = "StartTurn";
    }

    private void CyclePlayerIng()
    {
        List<int> noCard = new List<int>()
        {
            player1Ing.Count,
            player2Ing.Count,
            player3Ing.Count,
            player4Ing.Count
        };

        for (int shift = 0; shift < 4; shift++)
        {
            int noCardDiff = AllIngTransform[shift].childCount - noCard[(player + shift) % 4];

            if (noCardDiff > 0)
            {
                for (int count = 0; count < noCardDiff; count++) 
                {
                    Destroy(AllIngTransform[shift].GetChild(AllIngTransform[shift].childCount - count-1).gameObject);
                }
            }
            else if (noCardDiff < 0)
            {
                for (int count = 0; count > noCardDiff; count--)
                {
                    Instantiate(prefabIng, AllIngTransform[shift]).GetComponent<CardBehavior>().canvas = overlayCanvas;
                }
            }

            for (int idx = 0; idx < AllIngCard[(player + shift) % 4].Count; idx++)
            {
                IngredientCardViz Viz = AllIngTransform[shift].GetChild(idx).GetComponent<IngredientCardViz>();
                Viz.LoadCard(AllIngCard[(player + shift) % 4][idx]);
            }
        }
    }

    private void CyclePlayerHand()
    {

    }
}
