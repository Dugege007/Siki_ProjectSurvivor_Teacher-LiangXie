using UnityEngine;
using QFramework;
using System.Collections.Generic;

namespace ProjectSurvivor
{

    public partial class EnemyGenerator : ViewController
    {
        [SerializeField]
        public LevelConfig LevelConfig;

        private float mCurrentGenerateSeconds = 0;
        private float mCurrentWaveSeconds = 0;

        public static BindableProperty<int> EnemyCount = new BindableProperty<int>(0);

        private Queue<EnemyWave> mEnemyWavesQueue = new Queue<EnemyWave>();

        private EnemyWave mCurrentWave = null;
        public int WaveCount = 0;

        private int mTotalCount = 0;
        public bool IsLastWave => WaveCount == mTotalCount;

        public EnemyWave CurrentWave => mCurrentWave;

        private void Start()
        {
            foreach (var group in LevelConfig.EnemyWaveGroups)
            {
                foreach (var wave in group.Waves)
                {
                    mEnemyWavesQueue.Enqueue(wave);
                    mTotalCount++;
                }
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
                        // 获取屏幕边缘之外的随机位置
                        int xOry = RandomUtility.Choose(-1, 1);
                        Vector2 pos = Vector2.zero;
                        if (xOry == -1)
                        {
                            pos.x = RandomUtility.Choose(CameraController.LBTrans.position.x, CameraController.RTTrans.position.x);
                            pos.y = Random.Range(CameraController.LBTrans.position.y, CameraController.RTTrans.position.y);
                        }
                        else
                        {
                            pos.x = Random.Range(CameraController.LBTrans.position.x, CameraController.RTTrans.position.x);
                            pos.y = RandomUtility.Choose(CameraController.LBTrans.position.y, CameraController.RTTrans.position.y);
                        }

                        // 生成敌人
                        mCurrentWave.EnemyPrefab.Instantiate()
                            .Position(pos)
                            .Self(self =>
                            {
                                Enemy enemy = self.GetComponent<Enemy>();
                                enemy.SetSpeedScale(mCurrentWave.SpeedScale);
                                enemy.SetHPScale(mCurrentWave.HPScale);
                            })
                            .Show();
                    }
                }

                if (mCurrentWaveSeconds > mCurrentWave.WaveDurationSeconds)
                {
                    mCurrentWave = null;
                }
            }
        }
    }
}
