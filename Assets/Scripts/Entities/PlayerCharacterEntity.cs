public class PlayerCharacterEntity: AbstractCharacterEntity
{
    protected override void Start() {
        base.Start();
        entity.isPlayerCharacter = true;
    }
}