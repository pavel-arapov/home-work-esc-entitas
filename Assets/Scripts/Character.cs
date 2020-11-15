using System;
using UnityEngine;

public class Character : MonoBehaviour
{

    public GameEntity entity;
    public TargetIndicator targetIndicator;
    
    private static readonly int Shoot1 = Animator.StringToHash("Shoot");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Attack = Animator.StringToHash("MeleeAttack");

    private Animator _animator => GetComponentInChildren<Animator>();
    
    private void Start() {
        targetIndicator = GetComponentInChildren<TargetIndicator>(true);
        entity.AddTargetIndicator(targetIndicator.gameObject);
        entity.AddAnimator(_animator);
    }
    
    
    // public void Shoot() {
    //     _animator.SetTrigger(Shoot1);
    // }
    //
    // public void MeleeAttack() {
    //     _animator.SetTrigger(Attack);
    // }

    // public void RunningAnimation(float runSpeed) {
    //     _animator.SetFloat(Speed, runSpeed);
    // }

    // public void Idle() {
    //     _animator.SetFloat(Speed, 0.0f);
    // }

    public float GetHealth() {
        if (entity.hasHealth) {
            return entity.health.value;
        }

        return 0;
    }

    private void OnDestroy() {
        // entity.Destroy();
    }
}