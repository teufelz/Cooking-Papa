using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IngredientCard")]
public class IngredientCard : ScriptableObject
{
    public string title;
    public Sprite sprite;
    public bool bonus;
}
