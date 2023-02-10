using UnityEngine;
using MyCell.Interfaces;

namespace MyCell.Structs
{
    [System.Serializable]
    public struct OptionalAttachmentPointStruct : INameable
    {
        [HideInInspector]
        public string AttackName;
        public Sprite Sprite;
        public bool UseSprite;

        public void SetName(string value)
        {
            AttackName = value;
        }
    }
}
