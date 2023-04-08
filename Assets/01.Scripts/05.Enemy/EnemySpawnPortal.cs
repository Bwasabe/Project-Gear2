using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPortal : MonoBehaviour
{
    [SerializeField]
    private Enemy _enemyPrefab; // 나중에 StageData에서 가져오기

    [SerializeField]
    private int _enemySpawnCountMin = 3;

    [SerializeField]
    private int _enemySpawnCountMax = 5;

    [SerializeField]
    private float _enemySpawnRange = 2f;

    [SerializeField]
    private Vector3 _enemyOffset;
    
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + _enemyOffset, _enemySpawnRange);
    }
    #endif
}
