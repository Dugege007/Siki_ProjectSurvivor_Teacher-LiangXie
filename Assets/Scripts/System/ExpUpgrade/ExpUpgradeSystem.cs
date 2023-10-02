using UnityEngine;
using QFramework;
using System.Collections.Generic;
using System.Linq;

namespace ProjectSurvivor
{
    public class ExpUpgradeSystem : AbstractSystem
    {
        public static EasyEvent OnCoinUpgradeSystemChanged = new EasyEvent();

        public List<ExpUpgradeItem> Items { get; } = new List<ExpUpgradeItem>();

        protected override void OnInit()
        {
            ResetData();

            Global.Level.Register(_ =>
            {
                Roll();
            });
        }

        public ExpUpgradeItem Add(ExpUpgradeItem item)
        {
            Items.Add(item);
            return item;
        }

        public void ResetData()
        {
            Items.Clear();

            Add(new ExpUpgradeItem()
                .WithKey("simple_sword")
                .WithDescription((lv) =>
                {
                    return lv switch
                    {
                        1 => $"剑 Lv1：攻击身边的敌人",
                        2 => $"剑 Lv2" + "\n" + "攻击力+3 数量+2",
                        3 => $"剑 Lv3" + "\n" + "攻击力+2 间隔-0.25s",
                        4 => $"剑 Lv4" + "\n" + "攻击力+2 间隔-0.25s",
                        5 => $"剑 Lv5" + "\n" + "攻击力+3 数量+2",
                        6 => $"剑 Lv6" + "\n" + "范围+1 间隔-0.25s",
                        7 => $"剑 Lv7" + "\n" + "攻击力+3 数量+2",
                        8 => $"剑 Lv8" + "\n" + "攻击力+1 范围+1",
                        9 => $"剑 Lv9" + "\n" + "攻击力+3 间隔-0.25s",
                        10 => $"剑 Lv10" + "\n" + "攻击力+3 数量+2",
                        _ => null,
                    };
                })
                .WithMaxLevel(10)
                .OnUpgrade((_, level) =>
                {
                    switch (level)
                    {
                        case 1:
                            break;
                        case 2:
                            Global.SimpleSwordDamage.Value += 3;
                            Global.SimpleSwordCount.Value += 2;
                            break;
                        case 3:
                            Global.SimpleSwordDamage.Value += 2;
                            Global.SimpleSwordDuration.Value -= 0.25f;
                            break;
                        case 4:
                            Global.SimpleSwordDamage.Value += 2;
                            Global.SimpleSwordDuration.Value -= 0.25f;
                            break;
                        case 5:
                            Global.SimpleSwordDamage.Value += 3;
                            Global.SimpleSwordCount.Value += 2;
                            break;
                        case 6:
                            Global.SimpleSwordDuration.Value -= 0.25f;
                            Global.SimpleSwordRange.Value += 1;
                            break;
                        case 7:
                            Global.SimpleSwordDamage.Value += 3;
                            Global.SimpleSwordCount.Value += 2;
                            break;
                        case 8:
                            Global.SimpleSwordDamage.Value += 1;
                            Global.SimpleSwordRange.Value += 1;
                            break;
                        case 9:
                            Global.SimpleSwordDamage.Value += 3;
                            Global.SimpleSwordDuration.Value -= 0.25f;
                            break;
                        case 10:
                            Global.SimpleSwordDamage.Value += 3;
                            Global.SimpleSwordCount.Value += 2;
                            break;
                        default:
                            break;
                    }
                }));
        }

        public void Roll()
        {
            foreach (ExpUpgradeItem expUpgradeItem in Items)
            {
                expUpgradeItem.Visible.Value = false;
            }

            ExpUpgradeItem item = Items.Where(item => item.UpgradeFinish == false)
                .ToList()
                .GetRandomItem();   // 获取随机的单位

            Debug.Log(item.Key);

            if (item == null)
            {
                Debug.LogError("没有可用的升级项");
                return;
            }

            item.Visible.Value = true;
        }
    }
}
