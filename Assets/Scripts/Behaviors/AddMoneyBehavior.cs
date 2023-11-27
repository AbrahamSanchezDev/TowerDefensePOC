using UnityEngine;

namespace WorldsDev
{
    [CreateAssetMenu(menuName = "Behaviors/AddMoneyBehavior")]
    public class AddMoneyBehavior : SetupBehavior
    {
        [Range(1, 100)] public int MoneyToAdd = 1;

        public GameObject OnGenerateMoneyEffect;
        

        public override void SetupGo(GameObject go, SummonData data)
        {
            base.SetupGo(go, data);

            var money = go.AddComponent<AddMoneyObj>();
            money.Setup(MoneyToAdd, data.Speed,OnGenerateMoneyEffect);
        }
    }
}