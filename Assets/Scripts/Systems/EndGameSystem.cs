using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class EndGameSystem: ReactiveSystem<GameEntity>
{
    private IGroup<GameEntity> _playerCharacters;
    private IGroup<GameEntity> _enemyCharacters;
    private Contexts _contexts;

    public EndGameSystem(Contexts contexts) : 
        base(contexts.game) {

        _contexts = contexts;
        _playerCharacters = contexts.game.GetGroup(GameMatcher.AllOf(
            GameMatcher.PlayerCharacter, 
            GameMatcher.Health));
        _enemyCharacters = contexts.game.GetGroup(GameMatcher.AllOf(
            GameMatcher.EnemyCharacter, 
            GameMatcher.Health));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.StateIdle);
    }

    protected override bool Filter(GameEntity entity) {
        return entity.isStateIdle;
    }

    protected override void Execute(List<GameEntity> entities) {
        if (_contexts.gameState.gameStateEntity.isWaitingPlayerState || _contexts.gameState.gameStateEntity.isGameOverState) {
            return;
        }
        
        // have to check if we have any character and enemy alive 

        if (_playerCharacters.count == 0) {
            Debug.Log("Player Lose");
            _contexts.gameState.gameStateEntity.isGameOverState = true;
            _contexts.gameState.gameStateEntity.isAITurnState = false;
            _contexts.gameState.gameStateEntity.isPlayersTurnState = false;
            _contexts.gameState.gameStateEntity.gameOverScreen.value.enabled = true;
            _contexts.gameState.gameStateEntity.gameOverText.text.SetText("You lose, try again");
            return;
        }
        
        if (_enemyCharacters.count == 0) {
            Debug.Log("Enemy Lose");
            _contexts.gameState.gameStateEntity.isGameOverState = true;
            _contexts.gameState.gameStateEntity.isAITurnState = false;
            _contexts.gameState.gameStateEntity.isPlayersTurnState = false;
            _contexts.gameState.gameStateEntity.gameOverScreen.value.enabled = true;
            _contexts.gameState.gameStateEntity.gameOverText.text.SetText("Congratulations! You won!");
            return;
        }
        
        foreach (GameEntity gameEntity in entities) {
            if (gameEntity.isPlayerCharacter) {
                _contexts.gameState.gameStateEntity.isAITurnState = true;
                _contexts.gameState.gameStateEntity.isPlayersTurnState = false;
            }

            if (gameEntity.isEnemyCharacter) {
                _contexts.gameState.gameStateEntity.isWaitingPlayerState = true;
                _contexts.gameState.gameStateEntity.isAITurnState = false;
                
            }
        }
        
    }
}