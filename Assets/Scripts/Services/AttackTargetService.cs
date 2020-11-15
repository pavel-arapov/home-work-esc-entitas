using System;
using Entitas;
using UnityEngine;

public static class AttackTargetService
{
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private static readonly int MeleeAttack = Animator.StringToHash("MeleeAttack");
    private static readonly int Speed = Animator.StringToHash("Speed");

    public static void PrepareAttackTarget(IGroup<GameEntity> _enemies, IGroup<GameEntity> _characters) {
        GameEntity currentTarget = FindCurrentTarget(_enemies);

        if (currentTarget == null || currentTarget.isStateDead) {
            SwitchTargetService.SwitchTarget(_enemies);
            currentTarget = FindCurrentTarget(_enemies);
        }

        if (currentTarget != null && _characters.count > 0) {
            GameEntity attacker = FindAliveAttacker(_characters);

            if (attacker.hasWeapon) {
                switch (attacker.weapon.weapon) {
                    case Constants.Weapon.Pistol:
                        attacker.isStateIdle = false;
                        attacker.isStateAttack = true;
                        
                        attacker.animator.animator.SetTrigger(Shoot);
                        break;
                    case Constants.Weapon.Bat:
                    case Constants.Weapon.Fist:
                        attacker.isStateIdle = false;
                        attacker.isStateRunningToEnemy = true;

                        if (attacker.hasTargetPosition) {
                            attacker.ReplaceTargetPosition(currentTarget.position.value);
                        }
                        else {
                            attacker.AddTargetPosition(currentTarget.position.value);
                        }
                        
                        attacker.animator.animator.SetFloat(Speed, attacker.runSpeed.value);;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else {
                Debug.Log("Something wrong happened... ");
            }
        }
        else {
            Debug.Log("No attackers?... " + _characters.count);
        }
    }

    public static GameEntity FindCurrentTarget(IGroup<GameEntity> characters) {
        GameEntity currentTarget = null;
        foreach (GameEntity entity in characters) {
            if (!entity.isTarget) continue;
            currentTarget = entity;
            break;
        }

        return currentTarget;
    }

    public static GameEntity FindAliveAttacker(IGroup<GameEntity> characters) {
        GameEntity currentAttacker = null;
        foreach (GameEntity entity in characters) {
            if (!entity.isStateDead && entity.hasHealth) {
                currentAttacker = entity;
                break;
            }
        }

        return currentAttacker;
    }
    
    public static void ExecuteAttackTarget(GameEntity attacker) {
        if (attacker.hasWeapon) {
            switch (attacker.weapon.weapon) {
                case Constants.Weapon.Pistol:
                    attacker.animator.animator.SetTrigger(Shoot);
                    break;
                case Constants.Weapon.Bat:
                    attacker.animator.animator.SetTrigger(MeleeAttack);
                    break;
                case Constants.Weapon.Fist:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}