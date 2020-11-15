using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RunningSystem: IExecuteSystem
{
    private Contexts _contexts;
    private IGroup<GameEntity> _entites;
    private static readonly int Speed = Animator.StringToHash("Speed");

    public RunningSystem(Contexts contexts) {
        _contexts = contexts;

        _entites = contexts.game.GetGroup(
            GameMatcher.AnyOf(GameMatcher.StateRunningToEnemy,GameMatcher.StateRunningFromEnemy)
        );
    }

    public void Execute() {
        List<GameEntity> arrived = new List<GameEntity>();
        foreach (GameEntity gameEntity in _entites) {
            if (RunningService.RunTowards(gameEntity)) {
                arrived.Add(gameEntity);
                
            }
        }

        foreach (GameEntity entity in arrived) {
            if (entity.isStateRunningToEnemy) {
                entity.isStateRunningToEnemy = false;
                entity.isStateAttack = true;
            }

            if (entity.isStateRunningFromEnemy) {
                entity.isStateRunningFromEnemy = false;
                entity.isStateIdle = true;
                    
                entity.ReplaceRotation(entity.originalRotation.value);
                    
                // entity.view.gameObject.TryGetComponent<Character>(out Character character);
                // character.Idle();
                entity.animator.animator.SetFloat(Speed,0.0f);
            }
        }
    }
}