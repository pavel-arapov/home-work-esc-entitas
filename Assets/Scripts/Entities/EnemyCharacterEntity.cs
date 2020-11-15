public class EnemyCharacterEntity: AbstractCharacterEntity
{
    protected override void Start() {
        base.Start();
        entity.isEnemyCharacter = true;
    }
}