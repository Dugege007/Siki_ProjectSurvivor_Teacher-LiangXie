using UnityEngine;
using QFramework;
using System.Collections.Generic;

namespace ProjectSurvivor
{
    [System.Serializable]
    public class EnemyWave
    {
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
        public int WaveDuration = 10;
    }

    public partial class EnemyGenerator : ViewController
    {
        private float mCurrentGenerateSeconds = 0;
        private float mCurrentWaveSeconds = 0;

        public static BindableProperty<int> EnemyCount = new BindableProperty<int>(0);

        /// <summary>
        /// �����б�
        /// </summary>
        public List<EnemyWave> EnemyWaves = new List<EnemyWave>();
        private Queue<EnemyWave> mEnemyWavesQueue = new Queue<EnemyWave>();

        private EnemyWave mCurrentWave = null;
        public int WaveCount = 0;
        public bool IsLastWave => WaveCount == EnemyWaves.Count;
        public EnemyWave CurrentWave => mCurrentWave;

        private void Start()
        {
            foreach (var enemyWave in EnemyWaves)
            {
                mEnemyWavesQueue.Enqueue(enemyWave);
            }
        }

        private void Update()
        {
            if (mCurrentWave == null)
            {
                if (mEnemyWavesQueue.Count > 0)
                {
                    WaveCount++;
                    mCurrentWave = mEnemyWavesQueue.Dequeue();

                    mCurrentGenerateSeconds = 0;
                    mCurrentWaveSeconds = 0;
                }
            }

            if (mCurrentWave != null)
            {
                mCurrentGenerateSeconds += Time.deltaTime;
                mCurrentWaveSeconds += Time.deltaTime;

                if (mCurrentGenerateSeconds > mCurrentWave.GenerateDuration)
                {
                    mCurrentGenerateSeconds = 0;

                    Player player = Player.Default;
                    if (player != null)
                    {
                        // ����Ƕ�
                        float randomAngle = Random.Range(0, 360f);
                        // ��ȡ����
                        float randomRadius = randomAngle * Mathf.Deg2Rad;
                        // ���㷽��
                        Vector3 direction = new Vector3(Mathf.Cos(randomRadius), Mathf.Sin(randomRadius));
                        // �������ɵ�λ��
                        Vector3 generatePos = player.transform.position + direction * 10;

                        // ���ɵ���
                        mCurrentWave.EnemyPrefab.Instantiate()
                            .Position(generatePos)
                            .Show();
                    }
                }

                if (mCurrentWaveSeconds > mCurrentWave.WaveDuration)
                {
                    mCurrentWave = null;
                }
            }
        }
    }
}
