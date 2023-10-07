using QFramework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectSurvivor
{
    public class AchievementSystem : AbstractSystem
    {
        // 缓存所有 Items
        public List<AchievementItem> Items = new List<AchievementItem>();

        public static EasyEvent<AchievementItem> OnAchievementUnlocked = new EasyEvent<AchievementItem>();

        protected override void OnInit()
        {
            SaveSystem saveSystem = this.GetSystem<SaveSystem>();

            Add(new AchievementItem()
                .WithKey("3_minutes")
                .WithName("坚持三分钟")
                .WithDescirption("坚持三分钟\n奖励500金币")
                .WithIconName("achievement_time_icon")
                .Condition(() => Global.CurrentSeconds.Value >= 60 * 3)
                .OnUnlocked(_ => Global.Coin.Value += 500))
                .Load(saveSystem);

            Add(new AchievementItem()
                .WithKey("5_minutes")
                .WithName("坚持五分钟")
                .WithDescirption("坚持五分钟\n奖励1000金币")
                .WithIconName("achievement_time_icon")
                .Condition(() => Global.CurrentSeconds.Value >= 60 * 5)
                .OnUnlocked(_ => Global.Coin.Value += 1000))
                .Load(saveSystem);

            Add(new AchievementItem()
                .WithKey("10_minutes")
                .WithName("坚持十分钟")
                .WithDescirption("坚持十分钟\n奖励1000金币")
                .WithIconName("achievement_time_icon")
                .Condition(() => Global.CurrentSeconds.Value >= 60 * 10)
                .OnUnlocked(_ => Global.Coin.Value += 1000))
                .Load(saveSystem);

            Add(new AchievementItem()
                .WithKey("lv_10")
                .WithName("升到10级")
                .WithDescirption("第一次升到10级\n奖励500金币")
                .WithIconName("achievement_level_icon")
                .Condition(() => Global.Level.Value >= 10)
                .OnUnlocked(_ => Global.Coin.Value += 500))
                .Load(saveSystem);

            Add(new AchievementItem()
                .WithKey("lv_20")
                .WithName("升到20级")
                .WithDescirption("第一次升到20级\n奖励1000金币")
                .WithIconName("achievement_level_icon")
                .Condition(() => Global.Level.Value >= 20)
                .OnUnlocked(_ => Global.Coin.Value += 1000))
                .Load(saveSystem);

            Add(new AchievementItem()
                .WithKey("lv_30")
                .WithName("升到30级")
                .WithDescirption("第一次升到30级\n奖励1000金币")
                .WithIconName("achievement_level_icon")
                .Condition(() => Global.Level.Value >= 30)
                .OnUnlocked(_ => Global.Coin.Value += 1000))
                .Load(saveSystem);

            ActionKit.OnUpdate.Register(() =>
            {
                if (Time.frameCount % 10 == 0)
                {
                    foreach (var achievementItem in Items.Where(achievementItem => !achievementItem.Unlocked && achievementItem.ConditionCheck()))
                    {
                        achievementItem.Unlock(saveSystem);
                    }
                }
            });
        }

        public AchievementItem Add(AchievementItem item)
        {
            Items.Add(item);
            return item;
        }
    }
}
