using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemySpawnPortal : MonoBehaviour, IPoolInitAble
{
    [SerializeField]
    private Enemy _enemyPrefab; // 나중에 StageData에서 가져오기

    [SerializeField]
    private int _enemySpawnCountMin = 3;
    [SerializeField]
    private int _enemySpawnCountMax = 5;

    [SerializeField]
    private float _enemySpawnDuration = 0.7f;

    [SerializeField]
    private float _enemySpawnDelayMin = 0.8f;
    [SerializeField]
    private float _enemySpawnDelayMax = 1f;

    [SerializeField]
    private AnimationClip _enterAnimationClip;

    [SerializeField]
    private Color _enemySpawnColor;

    private AnimationEventHandler _animationEventHandler;

    private Animator _animator;

    private readonly int PORTAL_END_HASH = Animator.StringToHash("IsEnd");

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _animationEventHandler = _animator.GetComponent<AnimationEventHandler>();
        
    }

    private void Start()
    {
        _animationEventHandler.AddEvent("EndPortal", OnPortalEnd);
    }

    public void Init()
    {
        StartCoroutine(SpawnEnemy());
    }
    
    private void OnPortalEnd()
    {
        PoolManager.Destroy(gameObject);        
    }



    private IEnumerator SpawnEnemy()
    {
        yield return Yields.WaitForSeconds(_enterAnimationClip.length);
        int random = Random.Range(_enemySpawnCountMin, _enemySpawnCountMax);

        for (int i = 0; i < random; ++i)
        {
            GameObject enemy = PoolManager.Instantiate(_enemyPrefab.gameObject, transform.position, Quaternion.identity);
            enemy.transform.localScale = Vector3.zero;
            enemy.transform.DOScale(Vector3.one, _enemySpawnDuration);
            
            EnemySpawnManager.Instance.AddEnemy(enemy.transform);

            SpriteRenderer spriteRenderer = enemy.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.color = _enemySpawnColor;
            
            spriteRenderer.DOColor(Color.white, _enemySpawnDuration);

            yield return Yields.WaitForSeconds(_enemySpawnDuration);
            enemy.GetComponent<Enemy>().enabled = true;

            float randomDelay = Random.Range(_enemySpawnDelayMin, _enemySpawnDelayMax);

            yield return Yields.WaitForSeconds(randomDelay);
        }
        
        _animator.SetTrigger(PORTAL_END_HASH);
    }
    
}
