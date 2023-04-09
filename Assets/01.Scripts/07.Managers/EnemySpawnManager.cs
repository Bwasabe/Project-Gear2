using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnManager : MonoSingleton<EnemySpawnManager>
{
    [SerializeField]
    private GameObject _portalPrefab;
    
    [SerializeField]
    private List<Transform> _enemyTransformList = new(20);

    [SerializeField]
    private Transform _enemyBoundLT;
    [SerializeField]
    private Transform _enemyBoundRB;

    [SerializeField]
    private float _portalSpawnDurationMin = 2f;
    [SerializeField]
    private float _portalSpawnDurationMax = 3f;

    [SerializeField]
    private int _maxEnemyCount = 20;

    [SerializeField]
    private int _boundColum;
    [SerializeField]
    private int _boundRow;

    [SerializeField]
    private float _portalSpawnRange;

    [SerializeField]
    private bool _isDebug;
    
    [SerializeField]
    private List<Bound> _enemySpawnBound;

    protected override void Start()
    {
        StartCoroutine(SpawnPortalCoroutine());
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            SpawnEnemy();
        }
    }

    [ContextMenu("GenerateBound")]
    private void GenerateBound()
    {
        _enemySpawnBound.Clear();
        
        Vector3 rbPosition = _enemyBoundRB.position;

        Vector3 rtPosition = _enemyBoundLT.position;
        
        float colSize = (rbPosition.x - rtPosition.x) / _boundColum;
        float rowSize = (rbPosition.y - rtPosition.y) / _boundRow;

        Vector2 lt = rtPosition;
        Vector2 rb = new Vector2(rtPosition.x + colSize, rtPosition.y + rowSize);
        
        for (int y = 0; y < _boundRow + 1; ++y)
        {
            for (int x = 0; x < _boundColum; ++x)
            {
                _enemySpawnBound.Add(new Bound(lt,rb));

                lt.x += colSize;
                rb.x += colSize;
            }

            lt = rtPosition;
            rb = new Vector2(rtPosition.x + colSize, rtPosition.y + rowSize);

            lt.y += rowSize * y;
            rb.y += rowSize * y;
        }
    }

    private IEnumerator SpawnPortalCoroutine()
    {
        while (true)
        {
            if(_enemyTransformList.Count > _maxEnemyCount)
            {
                yield return null;

                yield break;
            }
            Transform character = CharacterManager.Instance.Knight.transform;
            if(character == null)
            {
                yield return null;
                yield break;
            }

            float randomDuration = Random.Range(_portalSpawnDurationMin, _portalSpawnDurationMax);

            yield return Yields.WaitForSeconds(randomDuration);
            SpawnPortalByDistance(character);
        }
        

    }

    private void SpawnPortalByDistance(Transform character)
    {
        List<Bound> boundList = new List<Bound>(_enemySpawnBound);
        
        foreach (Bound bound in _enemySpawnBound)
        {
            if(Vector3.Distance(bound.Center, character.transform.position) <= _portalSpawnRange)
            {
                boundList.Remove(bound);
            }
        }

        Bound randomBound = boundList[Random.Range(0, boundList.Count)];

        PoolManager.Instantiate(_portalPrefab, randomBound.GetRandomPos(), Quaternion.identity);
    }
    
    public Transform GetClosestEnemy(Transform character)
    {
        if(_enemyTransformList.Count <= 0) return null;
        
        Transform closestEnemy = null;
        float minSqrMagnitude = float.MaxValue;
        
        foreach (Transform enemy in _enemyTransformList)
        {
            if(!enemy.gameObject.activeSelf)
            {
                StartCoroutine(RemoveEnemy(enemy));
                continue;
            }
            
            
            float sqrMagnitude = (enemy.position - character.position).sqrMagnitude;
            
            if(sqrMagnitude < minSqrMagnitude)
            {
                minSqrMagnitude = sqrMagnitude;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }

    private IEnumerator RemoveEnemy(Transform enemy)
    {
        yield return null;
        _enemyTransformList.Remove(enemy);
    }

    private void SpawnEnemy()
    {
        GameObject enemy = PoolManager.Instantiate(_portalPrefab, Define.MousePos, Quaternion.identity);
    }

    public void AddEnemy(Transform enemyTransform)
    {
        _enemyTransformList.Add(enemyTransform);
    }
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(!_isDebug) return;
        Gizmos.color = Color.magenta;
        foreach (var bound in _enemySpawnBound)
        {
            Gizmos.DrawWireCube(bound.Center, bound.Size);
        }
    }
    #endif
}

