using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject copyCard;
    public Transform player1Ing;
    public Transform player2Ing;
    public Transform player3Ing;
    public Transform player4Ing;

    // Start is called before the first frame update
    GameObject go;
    int counter;
    Transform selectedPlayer;

    private void selectPlayer(KeyCode key)
    {
        if (key == KeyCode.Alpha1)
        {
            Debug.Log("Player1 is selected");
            selectedPlayer = player1Ing;
        }
        else if (key == KeyCode.Alpha2)
        {
            Debug.Log("Player2 is selected");
            selectedPlayer = player2Ing;
        }
        else if (key == KeyCode.Alpha3)
        {
            Debug.Log("Player3 is selected");
            selectedPlayer = player3Ing;
        }
        else if (key == KeyCode.Alpha4)
        {
            Debug.Log("Player4 is selected");
            selectedPlayer = player4Ing;
        }
    }
    private static List<GameObject> GetAllChilds(Transform ts)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < ts.childCount; i++)
        {
            list.Add(ts.GetChild(i).gameObject);
        }
        return list;
    }

    private void cyclePlayerIng()
    {
        // Cycle Player Ing. card 1=>4=>2=>3
        List<GameObject> children1 = GetAllChilds(player1Ing);
        List<GameObject> children2 = GetAllChilds(player2Ing);
        List<GameObject> children3 = GetAllChilds(player3Ing);
        List<GameObject> children4 = GetAllChilds(player4Ing);
        foreach (GameObject child in children1)
        {
            child.transform.parent = player4Ing;
        }
        foreach (GameObject child in children2)
        {
            child.transform.parent = player3Ing;
        }
        foreach (GameObject child in children3)
        {
            child.transform.parent = player1Ing;
        }
        foreach (GameObject child in children4)
        {
            child.transform.parent = player2Ing;
        }
    }

    void Start()
    {
        counter = 1;
        selectedPlayer = player1Ing;
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && e.type == EventType.KeyDown)
        {
            //Debug.Log(e.keyCode);
            selectPlayer(e.keyCode);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            // draw ing.
            go = Instantiate(copyCard, selectedPlayer.transform);
            go.name = "IngCard" + counter.ToString();
            counter++;
            Debug.Log(go.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            // discard ing.
            // Destroy(go);
            var child = selectedPlayer.transform.GetChild(0).gameObject; // Destroy by index
            Destroy(child);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            // cycle player
            cyclePlayerIng();
        }
    }
}
