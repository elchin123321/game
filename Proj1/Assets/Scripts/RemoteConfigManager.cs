using System.Collections;
using System.Collections.Generic;
using Unity.RemoteConfig;
using UnityEngine;

public class RemoteConfigManager : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }

    [SerializeField] Hero hero;
    void Awake()
    {
        FetchRemoteConfiguration();
    }
    public void SetConfigs()
    {


        hero.speed = ConfigManager.appConfig.GetFloat("hero_moveSpeed");
        hero.jumpForce = ConfigManager.appConfig.GetFloat("hero_jumpForce");
    }

    public void FetchRemoteConfiguration()
    {
        ConfigManager.FetchCompleted += ApplyRemoteSettings;
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
        Debug.Log("Fetched");
    }
    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        switch (configResponse.requestOrigin)
        {
            case ConfigOrigin.Default:
                Debug.Log("No Setting Loaded; using default values");
                break;
            case ConfigOrigin.Cached:
                Debug.Log("No settings loaded; using cached values");
                break;
            case ConfigOrigin.Remote:
                Debug.Log("New settings loaded");
                SetConfigs();
                break;
        }
    }
}
