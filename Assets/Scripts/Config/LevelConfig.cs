using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSurvivor
{
    [CreateAssetMenu]
    public class LevelConfig : ScriptableObject
    {
        public List<EnemyWaveGroup> EnemyWaveGroups = new List<EnemyWaveGroup>();
    }

    [System.Serializable]
    public class EnemyWaveGroup
    {
        public string Name;

        [TextArea]
        public string Description = string.Empty;

        public List<EnemyWave> Waves = new List<EnemyWave>();
    }

    [System.Serializable]
    public class EnemyWave
    {
        /// <summary>
        /// 波次名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Active = true;

        /// <summary>
        /// 生成时间间隔
        /// </summary>
        public float GenerateDuration = 1;

        /// <summary>
        /// 敌人预制体
        /// </summary>
        public GameObject EnemyPrefab;

        /// <summary>
        /// 波次轮换时间
        /// </summary>
        public int WaveDurationSeconds = 10;

        /// <summary>
        /// 生命值缩放控制
        /// </summary>
        public float HPScale = 1.0f;

        /// <summary>
        /// 移动速度缩放控制
        /// </summary>
        public float SpeedScale = 1.0f;
    }
}
