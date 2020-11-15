using System.Collections.Generic;
using Entitas;

public class ComputeDamageSystem: ReactiveSystem<GameEntity>
{
    private IGroup<GameEntity> _characters;

    private IGroup<GameEntity> _enemies;
    // private IGroup<GameEntity> _currentTarget;
    
    public ComputeDamageSystem(Contexts contexts) : base(contexts.game) {
        // _currentTarget = contexts.game.GetGroup(GameMatcher.AllOf(
            // GameMatcher.EnemyCharacter, GameMatcher.Target));
            _characters = contexts.game.GetGroup(
                GameMatcher.AllOf(GameMatcher.PlayerCharacter));
            _enemies = contexts.game.GetGroup(
                GameMatcher.AllOf(GameMatcher.EnemyCharacter));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.ComputeDamage);
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasHealth && !entity.isStateDead;
    }

    protected override void Execute(List<GameEntity> entities) {
        foreach (GameEntity entity in entities) {
            GameEntity currentTarget = null;
            if (entity.isPlayerCharacter) {
                currentTarget = AttackTargetService.FindCurrentTarget(_enemies);
            }

            if (entity.isEnemyCharacter) {
                currentTarget = AttackTargetService.FindCurrentTarget(_characters);
            }

            if (currentTarget != null) {
                if (currentTarget.hasDamage) {
                    currentTarget.ReplaceDamage(currentTarget.damage.value + 1);
                }
                else {
                    currentTarget.AddDamage(1);
                }
            }

            entity.isComputeDamage = false;
        }
    }
}