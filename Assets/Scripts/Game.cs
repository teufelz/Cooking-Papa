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

    void Start()
    {
        counter = 1;
        selectedPlayer = player1Ing;
    }

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
            //you don't have to instantiate at the transform's positio nand rotation, swap these for what suits your needs
            go = Instantiate(copyCard, selectedPlayer.transform);
            go.name = "IngCard" + counter.ToString();
            counter++;
            Debug.Log(go.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Destroy(go);
            var child = selectedPlayer.transform.GetChild(0).gameObject; // Destroy by index
            Destroy(child);
        }
    }
}
