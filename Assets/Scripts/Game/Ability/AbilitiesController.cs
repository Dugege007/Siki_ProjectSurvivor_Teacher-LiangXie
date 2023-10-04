using UnityEngine;
using QFramework;
using System.Linq;

namespace ProjectSurvivor
{
    public partial class AbilitiesController : ViewController, IController
    {
        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }

        private void Start()
        {
            HideAllAbilities();

            Global.SimpleSwordUnlocked.RegisterWithInitValue(unlocked =>
            {
                if (unlocked)
                    SimpleSword.Show();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.SimpleBombUnlocked.RegisterWithInitValue(unlocked =>
            {

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.RotateSwordUnlocked.RegisterWithInitValue(unlocked =>
            {
                if (unlocked)
                    RotateSword.Show();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.SimpleKnifeUnlocked.RegisterWithInitValue(unlocked =>
            {
                if (unlocked)
                    SimpleKnife.Show();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.BasketballUnlocked.RegisterWithInitValue(unlocked =>
            {
                if (unlocked)
                    BasketballAbility.Show();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 随机升级一个
            this.GetSystem<ExpUpgradeSystem>().Items
                .Where(item => item.IsWeapon)
                .ToList()
                .GetRandomItem()
                .Upgrade();

            Global.SuperBomb.RegisterWithInitValue(unlocked =>
            {
                if (unlocked)
                {
                    SuperBomb.Show();
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            if (Player.Default.IsDead)
                HideAllAbilities();
        }

        private void HideAllAbilities()
        {
            SimpleSword.Hide();
            RotateSword.Hide();
            SimpleKnife.Hide();
            BasketballAbility.Hide();
        }
    }
}
