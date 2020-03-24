using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventCardViz : MonoBehaviour
{
    public Text title;
    public Image template;
    public Text effect;
    public Image type;

    public RawImage buttonBGColor;
    public Text button;

    public EventCard card;

    void Start()
    {
        LoadCard(card);
    }

    public void LoadCard(EventCard c)
    {
        if (c == null)
            return;

        card = c;
        title.text = c.title;
        template.sprite = c.template;
        effect.text = c.effect;
        type.sprite = c.type;
    }
    public void setButton(string button)
    {
        if (button == null)
        {
            buttonBGColor.color = new Color(0f, 0f, 0f, 0f);
            this.button.text = "";
        }
        else
        {
            buttonBGColor.color = new Color(1f, 1f, 0f);
            this.button.text = button;
        }
    }

}
