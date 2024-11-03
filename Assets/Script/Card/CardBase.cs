using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//基礎データにはMonoBehaviourよりScriptableObjectが便利
[CreateAssetMenu]//ScriptableObjectの特徴
public class CardBase : ScriptableObject
{
    //カードの基礎データ
    //inspecterに公開するためのもの
    [SerializeField] new string name;
    [SerializeField] CardType type;
    [SerializeField] int number;
    [SerializeField] Sprite icon;
    [TextArea]
    [SerializeField] string description;

    //他のファイルに公開するためのもの
    public global::System.String Name { get => name;}
    public CardType Type { get => type;}
    public global::System.Int32 Number { get => number;}
    public Sprite Icon { get => icon;}
    public global::System.String Description { get => description;}
}

public enum CardType
{
    Clown,
    Princess,
    Spy,
    Assassin,
    Minister,
    Magician,
    Shougun,
    Prince,

}