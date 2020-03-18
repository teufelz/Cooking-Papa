using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DishCard")]
public class DishCard : ScriptableObject
{
    public string title;
    public List<Sprite> ingredients;
    public int score;
}
