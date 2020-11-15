using Entitas;
using UnityEngine;

public class AbstractCharacterEntity: MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected float health = 3f;
    [SerializeField] protected Constants.Weapon weapon = Constants.Weapon.Pistol;
    [SerializeField] protected float runSpeed = 0.1f;
    [SerializeField] protected float distanceFromEnemy = 0.5f;
    
    protected Contexts contexts { get; private set; }
    protected GameEntity entity { get; private set; }

    protected virtual void Start() {
        contexts = Contexts.sharedInstance;
        entity = contexts.game.CreateEntity();
        entity.AddPosition(transform.position);
        entity.AddRotation(transform.rotation);
        entity.AddHealth(health);
        entity.AddPrefab(prefab);
        entity.AddWeapon(weapon);
        entity.AddOriginalPosition(transform.position);
        entity.AddOriginalRotation(transform.rotation);
        entity.AddRunSpeed(runSpeed);
        entity.AddDistanceToEnemy(distanceFromEnemy);
        entity.isStateIdle = true;
        Destroy(gameObject);
    }
}