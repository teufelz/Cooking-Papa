using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehavior : MonoBehaviour
{
    // Don't forget to create collider
    public int player;
    public bool visible;
    public bool name;
    public bool cardType;

    public Transform canvas;

    private GameObject overlay;

    bool mouseIn = false;
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Script loaded");

        overlay = Instantiate(gameObject, canvas);
        overlay.transform.position = new Vector3(450, 200, 0);
        overlay.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        overlay.SetActive(false);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("Clicked");
            Debug.Log(gameObject.transform.position);
        }
        if (mouseIn == false)
        {
            // Debug.Log("over");
            mouseIn = true;
        }
        overlay.SetActive(true);
    }

    void OnMouseExit()
    {
        if (mouseIn == true)
        {
            // Debug.Log("mouse exit");
            mouseIn = false;
        }
        overlay.SetActive(false);
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
