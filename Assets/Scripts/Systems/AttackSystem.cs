using System.Collections.Generic;
using Entitas;

public class AttackSystem: ReactiveSystem<GameEntity>
{
    public AttackSystem(Contexts contexts) : base(contexts.game) {
        
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.StateAttack);
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasHealth && entity.hasWeapon;
    }

    protected override void Execute(List<GameEntity> entities) {
        foreach (GameEntity gameEntity in entities) {
            AttackTargetService.ExecuteAttackTarget(gameEntity);
            gameEntity.isStateAttack = false;
        }
    }
}