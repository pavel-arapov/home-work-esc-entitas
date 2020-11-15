using UnityEngine;

public class CharacterAnimationEvents : MonoBehaviour
{
    Character character;
    
    private static readonly int Speed = Animator.StringToHash("Speed");

    void Start() {
        character = GetComponentInParent<Character>();
    }

    void ShootEnd() {
        // character.SetState(Constants.CharacterState.Idle);
        character.entity.isStateIdle = true;
        character.entity.isStateAttack = false;
    }

    void AttackEnd() {
        // character.SetState(Character.State.RunningFromEnemy);
        character.entity.isStateRunningFromEnemy = true;
        character.entity.isStateAttack = false;
        character.entity.ReplaceTargetPosition(character.entity.originalPosition.value);
        character.entity.animator.animator.SetFloat(Speed, character.entity.runSpeed.value);;
    }

    void PunchEnd() {
        // character.SetState(Character.State.RunningFromEnemy);
        character.entity.isStateRunningFromEnemy = true;
        character.entity.isStateAttack = false;
        character.entity.ReplaceTargetPosition(character.entity.originalPosition.value);
        character.entity.animator.animator.SetFloat(Speed, character.entity.runSpeed.value);;
    }

    void Footstep() {
        // character.PlayFootstepSound();
    }

    void DoDamage() {
        character.entity.isComputeDamage = true;
        // character.PlayAttackSound();
        // character.GetComponent<MuzzleEffectBehaviour>()?.PlayEffect();
        // Character targetCharacter = character.target.GetComponent<Character>();
        // targetCharacter.GetComponent<HitEffectBehaviour>().PlayEffect();
        // targetCharacter.PlayReceiveDamageSound();
        // targetCharacter.DoDamage();
    }
}