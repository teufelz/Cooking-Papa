using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehavior : MonoBehaviour
{
    // Don't forget to create collider
    public int player;
    public bool visible;
    public bool cardType;

    public Transform canvas;

    private GameObject overlay;

    bool mouseIn = false;
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Script loaded");
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("Clicked");
            Debug.Log(gameObject.transform.position);

            // Get card title (or type) by accessing it script
            Debug.Log(gameObject.GetComponent<IngredientCardViz>().getTitle());
        }
        if (mouseIn == false)
        {
            overlay = Instantiate(gameObject, canvas);
            overlay.transform.position = new Vector3(450, 200, 0);
            overlay.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            // Debug.Log("over");
            mouseIn = true;
        }
        //overlay.SetActive(true);
    }

    void OnMouseExit()
    {
        if (mouseIn == true)
        {
            mouseIn = false;
        }
        Destroy(overlay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Destroy(overlay);
    }
}
