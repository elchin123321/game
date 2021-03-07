using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarLineRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 pos;
    private void Start()
    {
        startPos = lineRenderer.GetPosition(0);
        endPos = lineRenderer.GetPosition(1);
        pos = endPos;
    }

    public void RedceHealthBar (float damage)
    {
        startPos.x += damage/2f;
        endPos.x -= damage / 2f;
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
