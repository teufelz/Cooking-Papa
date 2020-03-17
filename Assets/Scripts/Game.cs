using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject copyCard;
    public Transform parent;
    // Start is called before the first frame update
    GameObject go;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            //you don't have to instantiate at the transform's positio nand rotation, swap these for what suits your needs
            go = Instantiate(copyCard, parent.transform);
            Debug.Log(go.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log(go.transform.position);
            go.transform.position = new Vector3(transform.position.x, -0.4f, -14f);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Destroy(go);
            var child = parent.transform.GetChild(0).gameObject; // Destroy by index
            Destroy(child);
        }
    }
}
