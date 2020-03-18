using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventCard")]
public class EventCard : ScriptableObject
{
    public string title;
    public Sprite template;
    public string effect;
    public Sprite type;
    public bool instant;
}
