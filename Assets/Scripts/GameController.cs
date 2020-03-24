using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject prefabIng;
    public GameObject prefabDish;
    public GameObject gameOver;
    public int WINSCORE;
    public GameObject prefabEvent;

    public List<Transform> AllIngTransform;
    public Transform dishTransform;
    public Transform eventTransform;

    public Transform overlayCanvas;

    private int turn;
    private string phase;
    public int player;
    private int winner;

    public List<int> scores;

    private List<IngredientCard> player1Ing;
    private List<IngredientCard> player2Ing;
    private List<IngredientCard> player3Ing;
    private List<IngredientCard> player4Ing;

    private List<IngredientCard> ingredientDeck;
    private List<EventCard> eventDeck;
    private List<DishCard> dishDeck;

    private List<EventCard> player1Usa;
    private List<EventCard> player2Usa;
    private List<EventCard> player3Usa;
    private List<EventCard> player4Usa;

    List<List<IngredientCard>> AllIngCard;
    List<List<EventCard>> AllUsaCard;
    List<DishCard> AllDishCard;

    private int ingredientIdx;
    private int eventIdx;
    private int dishIdx;

    private GameObject overlayDraw1;
    private GameObject overlayDraw2;
    private GameObject overlayDish;
    private GameObject overlayEvent;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        StartCoroutine(Controller());
    }

    private void Initialize()
    {
        winner = 0;
        turn = 1;
        player = turn % 4;
        phase = "StartTurn";

        scores = new List<int>() { 0, 0, 0, 0 };

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

        player1Usa = new List<EventCard>();
        player2Usa = new List<EventCard>();
        player3Usa = new List<EventCard>();
        player4Usa = new List<EventCard>();

        AllUsaCard = new List<List<EventCard>>()
        {
            player1Usa,
            player2Usa,
            player3Usa,
            player4Usa
        };

        overlayDraw1 = Instantiate(prefabIng, overlayCanvas);
        overlayDraw1.transform.position = new Vector3(200, 200, 0);
        overlayDraw1.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        overlayDraw2 = Instantiate(prefabIng, overlayCanvas);
        overlayDraw2.transform.position = new Vector3(700, 200, 0);
        overlayDraw2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        overlayDraw1.SetActive(false);
        overlayDraw2.SetActive(false);

        overlayEvent = Instantiate(prefabEvent, overlayCanvas);
        overlayEvent.transform.position = new Vector3(450, 200, 0);
        overlayEvent.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        overlayEvent.SetActive(false);

        AllDishCard = new List<DishCard>();

        GenerateIngDeck();
        GenerateEventDeck();
        GenerateDishDeck();
        InitializeDraw();
        assignIngredientButton(false, player - 1);
        assignDishButton(false);
    }

    private void GenerateDishDeck()
    {
        dishDeck = new List<DishCard>();
        string path = "Cards/Dish/Food";

        // load cards
        for (int count = 1; count < 16; count++) 
        {
            string realPath = path + count.ToString();
            if (count < 11)
            {
                for (int i = 0; i < 3; i++)
                {
                    dishDeck.Add(Resources.Load(realPath) as DishCard);
                }
            }
            else if (count < 15)
            {
                for (int i = 0; i < 2; i++)
                {
                    dishDeck.Add(Resources.Load(realPath) as DishCard);
                }
            }
            else
            {
                dishDeck.Add(Resources.Load(realPath) as DishCard);    
            }

        }

        // shuffle cards
        for (int i = 0; i < dishDeck.Count; i++)
        {
            DishCard temp = dishDeck[i];
            int randomIndex = Random.Range(i, dishDeck.Count);
            dishDeck[i] = dishDeck[randomIndex];
            dishDeck[randomIndex] = temp;
        }

        //set index to 0
        dishIdx = 0;
    }

    private void GenerateIngDeck()
    {
        ingredientDeck = new List<IngredientCard>();

        Dictionary<string, bool> paths = new Dictionary<string, bool>
        {
            ["Cards/Ingredient/MeatBonus"] = true,
            ["Cards/Ingredient/FishBonus"] = true,
            ["Cards/Ingredient/FlourBonus"] = true,
            ["Cards/Ingredient/SpinachBonus"] = true,
            ["Cards/Ingredient/Meat"] = false,
            ["Cards/Ingredient/Fish"] = false,
            ["Cards/Ingredient/Flour"] = false,
            ["Cards/Ingredient/Spinach"] = false
        };

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

    private void GenerateEventDeck()
    {
        eventDeck = new List<EventCard>();
        List<string> paths = new List<string>
        {
            "Cards/Event/Stonk",
            "Cards/Event/Reloaded",
            "Cards/Event/Lightning",
            "Cards/Event/Nothing",
        };

        // load cards
        foreach (string path in paths)
        {
            for (int i = 0; i < 3; i++)
                eventDeck.Add(Resources.Load(path) as EventCard);
        }

        // shuffle cards
        for (int i = 0; i < eventDeck.Count; i++)
        {
            EventCard temp = eventDeck[i];
            int randomIndex = Random.Range(i, eventDeck.Count);
            eventDeck[i] = eventDeck[randomIndex];
            eventDeck[randomIndex] = temp;
        }

        //set index to 0
        eventIdx = 0;
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
        DrawDishCard();
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

        overlayDraw1.SetActive(false);
        overlayDraw2.SetActive(false);

        if (AllIngCard[player - 1].Count <= 7)
        {
            AllIngCard[player - 1].Add(selected);
        }

        UpdateIngCard(0, player - 1);
        assignIngredientButton(false, player -1);

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

    private void UpdateDishCard()
    {
        int noCardDiff = dishTransform.childCount - AllDishCard.Count;

        if (noCardDiff > 0)
        {
            for (int count = 0; count < noCardDiff; count++)
            {
                Destroy(dishTransform.GetChild(dishTransform.childCount - count - 1).gameObject);
            }
        }
        else if (noCardDiff < 0)
        {
            for (int count = 0; count > noCardDiff; count--)
            {
                Instantiate(prefabDish, dishTransform).GetComponent<CardBehavior>().canvas = overlayCanvas;
            }
        }
        for (int idx = 0; idx < AllDishCard.Count; idx++)
        {
            DishCardViz Viz = dishTransform.GetChild(idx).GetComponent<DishCardViz>();
            Viz.LoadCard(AllDishCard[idx]);
        }
    }

    private void UpdateUsaCard( int playerIdx)
    {
        int noCardDiff = eventTransform.childCount - AllUsaCard[playerIdx].Count;

        if (noCardDiff > 0)
        {
            for (int count = 0; count < noCardDiff; count++)
            {
                Destroy(eventTransform.GetChild(eventTransform.childCount - count - 1).gameObject);
            }
        }
        else if (noCardDiff < 0)
        {
            for (int count = 0; count > noCardDiff; count--)
            {
                Instantiate(prefabEvent, eventTransform).GetComponent<CardBehavior>().canvas = overlayCanvas;
            }
        }

        for (int idx = 0; idx < AllUsaCard[playerIdx].Count; idx++)
        {
            EventCardViz Viz = eventTransform.GetChild(idx).GetComponent<EventCardViz>();
            Viz.LoadCard(AllUsaCard[playerIdx][idx]);
        }
    }

    IEnumerator WaitForDrawCard(IngredientCard choice1, IngredientCard choice2 , System.Action<IngredientCard> result)
    {
        bool selected = false;
        Debug.Log(choice1);
        Debug.Log(choice2);

        overlayDraw1.GetComponent<IngredientCardViz>().LoadCard(choice1);
        overlayDraw2.GetComponent<IngredientCardViz>().LoadCard(choice2);

        overlayDraw1.GetComponent<IngredientCardViz>().setButton("J");
        overlayDraw2.GetComponent<IngredientCardViz>().setButton("K");

        overlayDraw1.SetActive(true);
        overlayDraw2.SetActive(true);

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
        EventCard draw = eventDeck[eventIdx];
        eventIdx = (eventIdx + 1) % eventDeck.Count;

        overlayEvent.GetComponent<EventCardViz>().LoadCard(draw);

        overlayEvent.SetActive(true);

        yield return new WaitForSeconds(3);
        overlayEvent.SetActive(false);

        // check event type proceed to next phase (Instant or Usable) 
        if (draw.instant)
        {
            yield return StartCoroutine(InstantEventPhase(draw));
        }
        else
        {
            if (AllUsaCard[player - 1].Count < 3)
            {
                AllUsaCard[player - 1].Add(draw);
            }
            UpdateUsaCard(player - 1);
        }

        if (AllUsaCard[player - 1].Count > 0)
        {
            yield return StartCoroutine(UsableEventPhase());
        }

        phase = "Cooking";
    }

    IEnumerator InstantEventPhase(EventCard eventCard)
    {
        Debug.Log("Instant Phase");

        yield return new WaitForSeconds(1);
        
        //call instant func
        switch (eventCard.title)
        {
            case "Stonk":
                scores[player - 1] += 2;
                break;
            case "Lightning":
                int randomPlayer = Random.Range(0, 4);
                scores[randomPlayer] -= 2;
                break;
            case "Nothing":
                break;
        }
    }

    IEnumerator UsableEventPhase()
    {
        Debug.Log("Usable Phase");

        // use or skip
        EventCard selected = null;

        yield return StartCoroutine(WaitForUsable(AllUsaCard[player-1],value => selected = value));

        Debug.Log(selected);

        if (selected != null)
        {
            bool usingSuccessful = true;
            switch (selected.title)
            {
                case "Reloaded":
                    if (AllIngCard[player - 1].Count >= 9)
                    {
                        usingSuccessful = false;
                        break;
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        IngredientCard draw = ingredientDeck[ingredientIdx];
                        AllIngCard[player - 1].Add(draw);
                        ingredientIdx = (ingredientIdx + 1) % ingredientDeck.Count;
                    }

                    UpdateIngCard(0, player - 1);
                    break;
            }
            if (usingSuccessful)
            {
                AllUsaCard[player - 1].Remove(selected);
            }
            UpdateUsaCard(player - 1);
        }
    }

    IEnumerator WaitForUsable(List<EventCard> hand, System.Action<EventCard> result)
    {
        bool selected = false;
        assignEventButton(true, player - 1);
        while (!selected)
        {
            if (Input.GetKeyDown(KeyCode.J) && hand.Count>0)
            {
                selected = true;
                result(hand[0]);
            }
            if (Input.GetKeyDown(KeyCode.K) && hand.Count > 1)
            {
                selected = true;
                result(hand[1]);
            }
            if (Input.GetKeyDown(KeyCode.L) && hand.Count > 2)
            {
                selected = true;
                result(hand[2]);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                selected = true;
                result(null);
            }
            yield return null;
        }
        assignEventButton(false, player - 1);
    }

    IEnumerator CookingPhase()
    {
        Debug.Log("Cooking Phase");
        assignDishButton(true);
         yield return new WaitForSeconds(1);
        // cook or skip

        DishCard selected = null;

        yield return StartCoroutine(WaitForChoosingDish(value => selected = value));

        Debug.Log(selected);

        assignDishButton(false);

        if (selected != null)
        {
            yield return StartCoroutine(WaitForCooking(selected));
        }

        overlayDish.SetActive(false);
        phase = "EndTurn";
    }

    IEnumerator WaitForChoosingDish(System.Action<DishCard> result)
    {
        bool selected = false;

        overlayDish = Instantiate(prefabDish, overlayCanvas);
        overlayDish.transform.position = new Vector3(350, 300, 0);
        overlayDish.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        overlayDish.SetActive(false);

        while (!selected)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                selected = true;
                overlayDish.GetComponent<DishCardViz>().LoadCard(AllDishCard[0]);
                overlayDish.GetComponent<DishCardViz>().setButton(null);
                overlayDish.SetActive(true);
                result(AllDishCard[0]);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                selected = true;
                overlayDish.GetComponent<DishCardViz>().LoadCard(AllDishCard[1]);
                overlayDish.GetComponent<DishCardViz>().setButton(null);
                overlayDish.SetActive(true);
                result(AllDishCard[1]);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                selected = true;
                overlayDish.GetComponent<DishCardViz>().LoadCard(AllDishCard[2]);
                overlayDish.GetComponent<DishCardViz>().setButton(null);
                overlayDish.SetActive(true);
                result(AllDishCard[2]);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                selected = true;
                result(null);
            }
            yield return null;
        }
    }

    IEnumerator WaitForCooking(DishCard dishCard)
    {
        assignIngredientButton(true, player -1);
        List<IngredientCard> needed = new List<IngredientCard>(dishCard.ingredients);

        List<int> used = new List<int>();

        while (needed.Count > 0)
        {
            int idx = -1;

            if (Input.GetKeyDown(KeyCode.Q)) idx = 0;
            else if (Input.GetKeyDown(KeyCode.W)) idx = 1;
            else if (Input.GetKeyDown(KeyCode.E)) idx = 2;
            else if (Input.GetKeyDown(KeyCode.R)) idx = 3;
            else if (Input.GetKeyDown(KeyCode.T)) idx = 4;
            else if (Input.GetKeyDown(KeyCode.Y)) idx = 5;
            else if (Input.GetKeyDown(KeyCode.U)) idx = 6;
            else if (Input.GetKeyDown(KeyCode.I)) idx = 7;
            else if (Input.GetKeyDown(KeyCode.O)) idx = 8;
            else if (Input.GetKeyDown(KeyCode.P)) idx = 9;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                used = new List<int>();
                break;
            }

            if (idx >-1 && idx < AllIngCard[player - 1].Count)
            {
                IngredientCard pop = needed.Find(x => x.title == AllIngCard[player - 1][idx].title);
                if (pop != null)
                {
                    if (!used.Contains(idx))
                    {
                        used.Add(idx);
                        needed.Remove(pop);
                        IngredientCardViz Viz = AllIngTransform[0].GetChild(idx).GetComponent<IngredientCardViz>();
                        Viz.setSelected(true);
                        UpdateIngCard(0, player - 1);
                    }
                }
            }

            yield return null;
        }

        if (needed.Count == 0)
        {
            used.Sort();

            int bonus = 0;
            int shift = 0;
            foreach (int idx in used)
            {
                if (AllIngCard[player - 1][idx - shift].bonus)
                {
                    bonus++;
                }
                AllIngCard[player - 1].RemoveAt(idx - shift);
                shift++;
            }
            UpdateIngCard(0, player - 1);

            //update score
            scores[player - 1] += dishCard.score + bonus;

            //Check end game
            int _player = 0;
            foreach (int score in scores)
            {
                Debug.Log(score);
                _player++;
                if (score >= WINSCORE)
                {
                    Debug.Log(_player.ToString() + "Win");
                    winner = _player;
                    yield return StartCoroutine(GameEnd());
                    break;
                }
            }

            AllDishCard.Remove(dishCard);
            UpdateDishCard();
        }
        assignIngredientButton(false, player - 1);
    }

    public int getWinner()
    {
        return winner;
    }
    public string getPhase()
    {
        return phase;
    }

    IEnumerator GameEnd()
    {
        bool select = false;
        gameOver.SetActive(true);
        gameOver.GetComponent<GameOver>().show(winner);
        while (!select)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                select = true;
                Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
                Debug.Log("Scene Reloaded");
                break;
            }
            // Prevent game crash
            yield return null;
        }
        yield return null;
    }

    IEnumerator EndTurnPhase()
    {
        Debug.Log("End Phase");
        yield return new WaitForSeconds(1);
        //check dish & hand & score
        DrawDishCard();

        CyclePlayerIng();
        CyclePlayerHand();
        player = turn % 4 + 1;
        turn += 1;
        phase = "StartTurn";
    }

    private void DrawDishCard()
    {
        while (AllDishCard.Count < 3)
        {
            AllDishCard.Add(dishDeck[dishIdx]);
            dishIdx = (dishIdx + 1) % dishDeck.Count;
        }
        UpdateDishCard();
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

    private void assignDishButton(bool visible)
    {
        List<string> buttons = new List<string>() { "J", "K", "L" };
        if (visible)
        {
            //TODO: make button visible for dish
            for(int idx = 0; idx < AllDishCard.Count; idx++)
            {
                DishCardViz Viz = dishTransform.GetChild(idx).GetComponent<DishCardViz>();
                Viz.setButton(buttons[idx]);
            }
        }
        else
        {
            //TODO: make button invisible for dish
            for (int idx = 0; idx < AllDishCard.Count; idx++)
            {
                DishCardViz Viz = dishTransform.GetChild(idx).GetComponent<DishCardViz>();
                Viz.setButton(null);
            }
        }
    }

    private void assignIngredientButton(bool visible, int currentPlayer)
    {
        List<string> buttons = new List<string>() { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P" };

        if (visible)
        {
            for (int j = 0; j < AllIngCard[currentPlayer].Count; j++)
            {
                IngredientCardViz Viz = AllIngTransform[0].GetChild(j).GetComponent<IngredientCardViz>();
                Viz.setButton(buttons[j]);
            }
        }
        else
        {
            for (int j = 0; j < AllIngTransform[0].childCount; j++)
            {
                IngredientCardViz Viz = AllIngTransform[0].GetChild(j).GetComponent<IngredientCardViz>();
                Viz.setButton(null);
            }
        }
    }

    private void assignEventButton(bool visible, int currentPlayer)
    {
        List<string> buttons = new List<string>() { "J", "K", "L" };

        if (visible)
        {
            for (int j = 0; j < AllUsaCard[currentPlayer].Count; j++)
            {
                EventCardViz Viz = eventTransform.GetChild(j).GetComponent<EventCardViz>();
                Viz.setButton(buttons[j]);
            }
        }
        else
        {
            for (int j = 0; j < eventTransform.childCount; j++)
            {
                EventCardViz Viz = eventTransform.GetChild(j).GetComponent<EventCardViz>();
                Viz.setButton(null);
            }
        }
    }


    private void CyclePlayerHand()
    {
        UpdateUsaCard(player%4);
    }
}