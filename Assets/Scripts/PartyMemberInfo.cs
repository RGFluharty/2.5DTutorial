using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Party Member")]
public class PartyMemberInfo : ScriptableObject
{
    public string MemberName;
    public int StartingLevel;
    public int BaseHealth;
    public int BaseStr;
    public int BaseInit;
    public GameObject MemberBattleVisualPrefab; // What will be displayed in the battle scene
    public GameObject MemberOverworldVisualPrefab; // What will be displayed in the overworld scene
}
