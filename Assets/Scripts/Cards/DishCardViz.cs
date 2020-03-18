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

    private GameObject gameObject;

    void Start()
    {
        gameObject = new GameObject();
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
            GameObject obj = Instantiate(gameObject,art.transform);
            obj.name = "Ingredient";
            Image image = obj.AddComponent<Image>();

            image.sprite = ingredient.sprite;

        }

        score.text = c.score.ToString();
    } 

}
