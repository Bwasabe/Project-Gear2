using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    [SerializeField]
    private GameObject _enemyPrefab;

    private readonly List<Transform> _enemyTransformList = new();
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            SpawnEnemy();
        }
    }
    
    public Transform GetClosestEnemy(Transform character)
    {
        if(_enemyTransformList.Count <= 0) return null;
        
        Transform closestEnemy = null;
        float minSqrMagnitude = float.MaxValue;
            
        foreach (Transform enemy in _enemyTransformList)
        {
            if(!enemy.gameObject.activeSelf) continue;
            
            float sqrMagnitude = (enemy.position - character.position).sqrMagnitude;
            if(sqrMagnitude < minSqrMagnitude)
            {
                minSqrMagnitude = sqrMagnitude;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(_enemyPrefab, Define.MousePos, Quaternion.identity);
        _enemyTransformList.Add(enemy.transform);
    }
}
