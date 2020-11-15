using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AITurnSystem: ReactiveSystem<GameStateEntity>
{
    private Contexts _contexts;
    private IGroup<GameEntity> _characters;
    private GameStateEntity _gameStateEntity;
    private IGroup<GameEntity> _enemies;

    public AITurnSystem(Contexts contexts) : base(contexts.gameState) {
        _contexts = contexts;
        
        _characters = contexts.game.GetGroup(
            GameMatcher.AllOf(GameMatcher.PlayerCharacter));
        _enemies = contexts.game.GetGroup(
            GameMatcher.AllOf(GameMatcher.EnemyCharacter));
    }

    protected override ICollector<GameStateEntity> GetTrigger(IContext<GameStateEntity> context) {
        return context.CreateCollector(GameStateMatcher.AITurnState);
    }

    protected override bool Filter(GameStateEntity entity) {
        return entity.isAITurnState;
    }

    protected override void Execute(List<GameStateEntity> entities) {
        Debug.Log("Enemy attack started");
        foreach (var gameStateEntity in entities) {
            if (gameStateEntity.isAITurnState) {
                // initiate enemy attack
                AttackTargetService.PrepareAttackTarget( _characters, _enemies);
                gameStateEntity.isAITurnState = false;
            }
        }
    }
}