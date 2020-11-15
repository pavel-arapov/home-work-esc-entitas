using System.Collections.Generic;
using Entitas;

public class ApplyDamageSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    private InputEntity[] _switchInput;

    public ApplyDamageSystem(Contexts contexts) 
        : base(contexts.game) {
        _contexts = contexts;
        _switchInput = _contexts.input.GetEntities(InputMatcher.SwitchInput);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return new Collector<GameEntity>(
            new [] {
                context.GetGroup(GameMatcher.Damage),
            }, new [] {
                GroupEvent.Added,
            }
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.isPlayerCharacter || entity.isEnemyCharacter || entity.hasHealth;
    }

    protected override void Execute(List<GameEntity> entities) {
        foreach (GameEntity e in entities) {
            if (e.hasHealth) {
                e.health.value -= e.damage.value;
                e.RemoveDamage();
                if (e.health.value <= 0f) {
                    e.isStateDead = true;
                    e.isStateIdle = false;
                    e.RemoveHealth();
                    // if the enemy was killed, switch the target to alive one
                    if (e.isEnemyCharacter) {
                        _switchInput[0].isClick = true;
                    }
                }
            }
            else {
                e.RemoveDamage();
            }
        }
    }
}
