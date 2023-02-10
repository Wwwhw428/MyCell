namespace MyCell.Structs
{
    // 有这个结构方便实现反伤之类的功能
    public struct DamageData
    {
        public float DamageAmount;
        public UnityEngine.GameObject Source;

        public void SetData(UnityEngine.GameObject source, int damageAmount)
        {
            DamageAmount = damageAmount;
            Source = source;
        }
    }
}

