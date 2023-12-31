﻿
using UnityEngine;

namespace ProjectSurvivor
{
    public class DamageSystem
    {
        public static void CalculateDamage(float baseDamage, IEnemy enemy, int maxNormalDamage = 2, float criticalDamageTimes = 3)
        {
            baseDamage += baseDamage * Global.AdditionalDamage.Value;

            if (Random.Range(0, 1.0f) < Global.CriticalChance.Value)
            {
                enemy.Hurt(baseDamage * Random.Range(2f, criticalDamageTimes), false, true);
            }
            else
            {
                enemy.Hurt(Mathf.Max(1, baseDamage + Random.Range(-1, maxNormalDamage)));
            }
        }
    }
}
