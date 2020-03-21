using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DishCardViz : MonoBehaviour
{
    public Text title;
    public GameObject art;
    public Text score;

    public DishCard card;

    void Start()
    {
        LoadCard(card);
    }

    public void LoadCard(DishCard c)
    {
        if (c == null)
            return;

        card = c;
        title.text = c.title;

        int noArtDiff = art.transform.childCount - c.ingredients.Count;

        if (noArtDiff > 0)
        {
            for (int count = 0; count < noArtDiff; count++)
            {
                Destroy(art.transform.GetChild(art.transform.childCount - count - 1).gameObject);
            }
        }
        else if (noArtDiff < 0)
        {
            for (int count = 0; count > noArtDiff; count--)
            {
                GameObject obj = Instantiate(new GameObject(), art.transform);
                obj.name = "Ingredient";
                obj.AddComponent<Image>();
            }
        }

        for (int idx = 0; idx < c.ingredients.Count; idx++)
        {
            art.transform.GetChild(idx).GetComponent<Image>().sprite = c.ingredients[idx].sprite;
        }
        score.text = c.score.ToString();
    } 

}
