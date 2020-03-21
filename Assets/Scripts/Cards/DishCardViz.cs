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
        
        foreach (IngredientCard ingredient in c.ingredients)
        {
            int idx = 0;
            GameObject obj;
            Image image;
            if (art.transform.childCount < c.ingredients.Count)
            {
                obj = Instantiate(new GameObject(),art.transform);
                obj.name = "Ingredient";
                image = obj.AddComponent<Image>();
            }
            else
            {
                obj = art.transform.GetChild(idx).gameObject;
                image = obj.GetComponent<Image>();
            }

            image.sprite = ingredient.sprite;
            idx++;
        }

        score.text = c.score.ToString();
    } 

}
