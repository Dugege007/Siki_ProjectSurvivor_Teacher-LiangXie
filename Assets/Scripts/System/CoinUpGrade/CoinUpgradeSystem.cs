using QFramework;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSurvivor
{
    public class CoinUpgradeSystem : AbstractSystem, ICanSave
    {
        public static EasyEvent OnCoinUpgradeSystemChanged = new EasyEvent();

        public List<CoinUpgradeItem> Items { get; } = new List<CoinUpgradeItem>();

        protected override void OnInit()
        {
            Add(new CoinUpgradeItem()
                .WithKey("exp_percent_lv1")
                .WithDescription("����ֵ ������� +2%")
                .WithPrice(20)
                .OnUpgrade(item =>
                {
                    Global.ExpPercent.Value += 0.02f;
                    Global.Coin.Value -= item.Price;
                }))
                .Next(Add(new CoinUpgradeItem()
                .WithKey("exp_percent_lv2")
                .WithDescription("����ֵ ������� +2%")
                .WithPrice(200)
                .OnUpgrade(item =>
                {
                    Global.ExpPercent.Value += 0.02f;
                    Global.Coin.Value -= item.Price;
                })))
                .Next(Add(new CoinUpgradeItem()
                .WithKey("exp_percent_lv3")
                .WithDescription("����ֵ ������� +2%")
                .WithPrice(500)
                .OnUpgrade(item =>
                {
                    Global.ExpPercent.Value += 0.02f;
                    Global.Coin.Value -= item.Price;
                })))
                .Next(Add(new CoinUpgradeItem()
                .WithKey("exp_percent_lv4")
                .WithDescription("����ֵ ������� +2%")
                .WithPrice(1000)
                .OnUpgrade(item =>
                {
                    Global.ExpPercent.Value += 0.02f;
                    Global.Coin.Value -= item.Price;
                })))
                .Next(Add(new CoinUpgradeItem()
                .WithKey("exp_percent_lv5")
                .WithDescription("����ֵ ������� +2%")
                .WithPrice(3000)
                .OnUpgrade(item =>
                {
                    Global.ExpPercent.Value += 0.02f;
                    Global.Coin.Value -= item.Price;
                })))
                .Next(Add(new CoinUpgradeItem()
                .WithKey("exp_percent_lv6")
                .WithDescription("����ֵ ������� +2%")
                .WithPrice(5000)
                .OnUpgrade(item =>
                {
                    Global.ExpPercent.Value += 0.02f;
                    Global.Coin.Value -= item.Price;
                })))
                .Next(Add(new CoinUpgradeItem()
                .WithKey("exp_percent_lv7")
                .WithDescription("����ֵ ������� +2%")
                .WithPrice(7500)
                .OnUpgrade(item =>
                {
                    Global.ExpPercent.Value += 0.02f;
                    Global.Coin.Value -= item.Price;
                })))
                .Next(Add(new CoinUpgradeItem()
                .WithKey("exp_percent_lv8")
                .WithDescription("����ֵ ������� +2%")
                .WithPrice(10000)
                .OnUpgrade(item =>
                {
                    Global.ExpPercent.Value += 0.02f;
                    Global.Coin.Value -= item.Price;
                })))
                .Next(Add(new CoinUpgradeItem()
                .WithKey("exp_percent_lv9")
                .WithDescription("����ֵ ������� +2%")
                .WithPrice(15000)
                .OnUpgrade(item =>
                {
                    Global.ExpPercent.Value += 0.02f;
                    Global.Coin.Value -= item.Price;
                })))
                .Next(Add(new CoinUpgradeItem()
                .WithKey("exp_percent_lv10")
                .WithDescription("����ֵ ������� +2%")
                .WithPrice(20000)
                .OnUpgrade(item =>
                {
                    Global.ExpPercent.Value += 0.02f;
                    Global.Coin.Value -= item.Price;
                })));


            Add(new CoinUpgradeItem()
                .WithKey("coin_percent_lv1")
                .WithDescription("��� ������� +5%")
                .WithPrice(100)
                .OnUpgrade(item =>
                {
                    Global.CoinPercent.Value += 0.05f;
                    Global.Coin.Value -= item.Price;
                }))
                .Next(Add(new CoinUpgradeItem()
                .WithKey("coin_percent_lv2")
                .WithDescription("��� ������� +5%")
                .WithPrice(600)
                .OnUpgrade(item =>
                {
                    Global.CoinPercent.Value += 0.05f;
                    Global.Coin.Value -= item.Price;
                })))
                .Next(Add(new CoinUpgradeItem()
                .WithKey("coin_percent_lv3")
                .WithDescription("��� ������� +5%")
                .WithPrice(2000)
                .OnUpgrade(item =>
                {
                    Global.CoinPercent.Value *= 0.05f;
                    Global.Coin.Value -= item.Price;
                })))
                .Next(Add(new CoinUpgradeItem()
                .WithKey("coin_percent_lv4")
                .WithDescription("��� ������� +5%")
                .WithPrice(6000)
                .OnUpgrade(item =>
                {
                    Global.CoinPercent.Value *= 0.05f;
                    Global.Coin.Value -= item.Price;
                })))
                .Next(Add(new CoinUpgradeItem()
                .WithKey("coin_percent_lv5")
                .WithDescription("��� ������� +5%")
                .WithPrice(15000)
                .OnUpgrade(item =>
                {
                    Global.CoinPercent.Value *= 0.05f;
                    Global.Coin.Value -= item.Price;
                })));


            Add(new CoinUpgradeItem()
                .WithKey("max_hp_lv1")
                .WithDescription("�������ֵ+1")
                .WithPrice(900)
                .OnUpgrade(item =>
                {
                    Global.MaxHP.Value++;
                    Global.Coin.Value -= item.Price;
                }))
                .Next(Add(new CoinUpgradeItem()
                .WithKey("max_hp_lv2")
                .WithDescription("�������ֵ+1")
                .WithPrice(2000)
                .OnUpgrade(item =>
                {
                    Global.MaxHP.Value++;
                    Global.Coin.Value -= item.Price;
                })))
                .Next(Add(new CoinUpgradeItem()
                .WithKey("max_hp_lv3")
                .WithDescription("�������ֵ+1")
                .WithPrice(5000)
                .OnUpgrade(item =>
                {
                    Global.MaxHP.Value++;
                    Global.Coin.Value -= item.Price;
                })))
                .Next(Add(new CoinUpgradeItem()
                .WithKey("max_hp_lv4")
                .WithDescription("�������ֵ+1")
                .WithPrice(12000)
                .OnUpgrade(item =>
                {
                    Global.MaxHP.Value++;
                    Global.Coin.Value -= item.Price;
                })))
                .Next(Add(new CoinUpgradeItem()
                .WithKey("max_hp_lv5")
                .WithDescription("�������ֵ+1")
                .WithPrice(25000)
                .OnUpgrade(item =>
                {
                    Global.MaxHP.Value++;
                    Global.Coin.Value -= item.Price;
                })));


            Add(new CoinUpgradeItem()
                .WithKey("treasure_chest_percent_lv1")
                .WithDescription("���� ������� +0.2%")
                .WithPrice(18000)
                .OnUpgrade(item =>
                {
                    Global.TreasureChestPercent.Value += 0.002f;
                    Global.Coin.Value -= item.Price;
                }))
                .Next(Add(new CoinUpgradeItem()
                .WithKey("treasure_chest_percent_lv2")
                .WithDescription("���� ������� +0.2%")
                .WithPrice(30000)
                .OnUpgrade(item =>
                {
                    Global.TreasureChestPercent.Value += 0.002f;
                    Global.Coin.Value -= item.Price;
                })));

            Load();

            OnCoinUpgradeSystemChanged.Register(() =>
            {
                Save();
            });
        }

        /// <summary>
        /// ����ǰ��������ӵ��б���
        /// </summary>
        /// <param name="item">��ǰ������</param>
        /// <returns>���ص�ǰ������</returns>
        /// <remark>��������ʽ����</remark>
        public CoinUpgradeItem Add(CoinUpgradeItem item)
        {
            Items.Add(item);
            return item;
        }

        public void Save()
        {
            SaveSystem saveSystem = this.GetSystem<SaveSystem>();

            foreach (var coinUpgradeItem in Items)
            {
                saveSystem.SaveBool(coinUpgradeItem.Key, coinUpgradeItem.UpgradeFinish);
            }
        }

        public void Load()
        {
            SaveSystem saveSystem = this.GetSystem<SaveSystem>();

            foreach (var coinUpgradeItem in Items)
            {
                coinUpgradeItem.UpgradeFinish = saveSystem.LoadBool(coinUpgradeItem.Key, false);
            }
        }
    }
}
