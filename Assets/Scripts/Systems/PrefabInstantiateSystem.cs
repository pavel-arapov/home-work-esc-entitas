using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class PrefabInstantiateSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public PrefabInstantiateSystem(Contexts contexts)
        : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Prefab);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPrefab && !entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities) {
            var obj = GameObject.Instantiate(e.prefab.prefab);
            if (obj.TryGetComponent<EntitasEntity>(out var ee))
                ee.entity = e;
            else
                obj.AddComponent<EntitasEntity>().entity = e;
            if (e.isPlayerCharacter || e.isEnemyCharacter) {
                if (obj.TryGetComponent<Character>(out var c)) {
                    c.entity = e;
                }
                else {
                    obj.AddComponent<Character>().entity = e;
                }
                
                
            }
            e.AddView(obj);
        }
    }
}