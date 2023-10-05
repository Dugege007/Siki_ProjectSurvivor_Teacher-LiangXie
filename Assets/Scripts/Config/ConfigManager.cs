using ProjectSurvivor;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager Instance;

    public AbilityConfig SimpleSwordConfig;
    public AbilityConfig RotateSwordConfig;
    public AbilityConfig SimpleKnifeConfig;
    public AbilityConfig BasketballConfig;
    public AbilityConfig SimpleBombConfig;
    public AbilityConfig CriticalChanceConfig;
    public AbilityConfig AdditionalExpRateConfig;
    public AbilityConfig AdditionalDamageConfig;
    public AbilityConfig AdditionalMovementSpeedConfig;
    public AbilityConfig AdditionalFlyThingCountConfig;
    public AbilityConfig CollectableAreaRangeConfig;

    private void Awake()
    {
        // 单例模式，确保只有一个ConfigManager实例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

    }
}
