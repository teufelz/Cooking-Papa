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

}
