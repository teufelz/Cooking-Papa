using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientCardViz : MonoBehaviour
{
    public Text title;
    public Image art;

    public Image bonus;
    public RawImage bonusBGColor;

    public bool isButtonVisible;
    public Text button;
    public RawImage buttonBGColor;

    public IngredientCard card;

    public string getTitle()
    {
        return title.text;
    }

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
            bonusBGColor.color = new Color(1f, 1f, 1f);
        }
        else
        {
            bonus.gameObject.SetActive(false);
            bonusBGColor.color = new Color(0f, 0f, 0f, 0f);
        }
    }
    
    public void setButton(string button)
    {
        if(button == null)
        {
            buttonBGColor.color = new Color(0f, 0f, 0f, 0f);
            this.button.text = "";
        }
        else
        {
            buttonBGColor.color = new Color(0f, 1f, 0.1f);
            this.button.text = button;
        }
    }

    public void setSelected(bool selected)
    {
        if (selected)
        {
            buttonBGColor.color = new Color(0f, 0.6f, 0f);
        }
        else
        {
            buttonBGColor.color = new Color(0f, 1f, 0.1f);
        }
    }
}
