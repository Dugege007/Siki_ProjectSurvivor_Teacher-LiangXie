using UnityEngine;
using QFramework;

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
            SimpleSword.Hide();
            RotateSword.Hide();
            SimpleKnife.Hide();
            BasketballAbility.Hide();

            Global.SimpleSwordUnlocked.RegisterWithInitValue(unlocked =>
            {
                if (unlocked)
                    SimpleSword.Show();

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
            this.GetSystem<ExpUpgradeSystem>().Items.GetRandomItem().Upgrade();
        }
    }
}
