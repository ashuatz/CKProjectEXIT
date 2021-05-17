using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    private CollisionEventRiser eventRiser;

    [SerializeField]
    private Rigidbody rigid;

    [SerializeField]
    private float Speed = 6f;


    private void Awake()
    {
        eventRiser.OnTriggerEnterEvent += EventRiser_OnTriggerEnterEvent;
    }

    private void EventRiser_OnTriggerEnterEvent(Collider obj)
    {
        if (obj.TryGetComponent<TestEntity>(out var entity))
        {
            entity.TakeDamage(new HitInfo());

            PoolManager.ReleaseObject(gameObject);
        }
    }

    public void Intialize(in Vector3 dir)
    {
        rigid.velocity = dir.normalized * Speed;

        StartCoroutine(DelayRelease());
    }

    IEnumerator DelayRelease()
    {
        yield return YieldInstructionCache.WaitForSeconds(10f);
        if (gameObject.activeInHierarchy)
        {
            PoolManager.ReleaseObject(gameObject);
        }
    }

}