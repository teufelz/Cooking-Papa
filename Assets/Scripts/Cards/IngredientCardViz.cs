using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientCardViz : MonoBehaviour
{
    public Text title;
    public Image art;
    public Image bonus;

    public IngredientCard card;

    void Start()
    {
        LoadCard(card);
    }

    public void LoadCard(IngredientCard c)
    {
        if (c == null)
            return;

        card = c;
        title.text = c.title;
        art.sprite = c.sprite;

        if (c.bonus)
        {
            bonus.gameObject.SetActive(true);
        }
        else
        {
            bonus.gameObject.SetActive(false);
        }


    } 

}
