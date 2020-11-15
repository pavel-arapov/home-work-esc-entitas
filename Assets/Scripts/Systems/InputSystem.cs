using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class InputSystem : ReactiveSystem<InputEntity>
{
    private Contexts _contexts;
    private IGroup<InputEntity> _entities;
    private IGroup<GameEntity> _characters;
    private IGroup<GameEntity> _enemies;

    public InputSystem(Contexts contexts) : base(contexts.input) {
        _contexts = contexts;
        _characters = contexts.game.GetGroup(
            GameMatcher.AllOf(GameMatcher.PlayerCharacter, GameMatcher.Health));
        _enemies = contexts.game.GetGroup(
            GameMatcher.AllOf(GameMatcher.EnemyCharacter));
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) {
        return context.CreateCollector(InputMatcher.Click);
    }

    protected override bool Filter(InputEntity entity) {
        return entity.isClick;
    }

    protected override void Execute(List<InputEntity> entities) {
        // input from Player allowed only in a specific game state
        Debug.Log("Execute Input System");
        foreach (var entity in entities) {
            if (!_contexts.gameState.gameStateEntity.isWaitingPlayerState) {
                entity.isClick = false;
                continue;
            }
            if (entity.isAttackInput) {
                AttackTargetService.PrepareAttackTarget(_enemies, _characters);
                _contexts.gameState.gameStateEntity.isWaitingPlayerState = false;
                _contexts.gameState.gameStateEntity.isPlayersTurnState = true;
            }

            if (entity.isSwitchInput) {
                Debug.Log("Launch Switch Target");
                SwitchTargetService.SwitchTarget(_enemies);
            }

            entity.isClick = false;
        }
    }
}