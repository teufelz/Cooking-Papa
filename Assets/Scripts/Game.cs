using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject copyCard;
    public Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            //you don't have to instantiate at the transform's positio nand rotation, swap these for what suits your needs
            var go = Instantiate(copyCard, transform.position, transform.rotation);
            go.transform.parent = parent;
            go.transform.localScale = new Vector3(1, 1, 1);
            go.transform.Rotate(25, 0, 0);
            go.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            Debug.Log(go.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {

        }
    }
}
