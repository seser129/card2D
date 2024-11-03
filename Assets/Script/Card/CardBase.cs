using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��b�f�[�^�ɂ�MonoBehaviour���ScriptableObject���֗�
[CreateAssetMenu]//ScriptableObject�̓���
public class CardBase : ScriptableObject
{
    //�J�[�h�̊�b�f�[�^
    //inspecter�Ɍ��J���邽�߂̂���
    [SerializeField] new string name;
    [SerializeField] CardType type;
    [SerializeField] int number;
    [SerializeField] Sprite icon;
    [TextArea]
    [SerializeField] string description;

    //���̃t�@�C���Ɍ��J���邽�߂̂���
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