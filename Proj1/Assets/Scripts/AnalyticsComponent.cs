using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnPlayerDead()
    {
        Analytics.CustomEvent("Played Dead");
    }
    public void OnEnemyKilled()
    {
        Analytics.CustomEvent("Enemy killed");
    }
}
