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
        /// ��������
        /// </summary>
        public string Name;

        /// <summary>
        /// �Ƿ񼤻�
        /// </summary>
        public bool Active = true;

        /// <summary>
        /// ����ʱ����
        /// </summary>
        public float GenerateDuration = 1;

        /// <summary>
        /// ����Ԥ����
        /// </summary>
        public GameObject EnemyPrefab;

        /// <summary>
        /// �����ֻ�ʱ��
        /// </summary>
        public int WaveDurationSeconds = 10;

        /// <summary>
        /// ����ֵ���ſ���
        /// </summary>
        public float HPScale = 1.0f;

        /// <summary>
        /// �ƶ��ٶ����ſ���
        /// </summary>
        public float SpeedScale = 1.0f;
    }
}
