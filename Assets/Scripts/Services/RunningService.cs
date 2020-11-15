using UnityEngine;

public static class RunningService
{
    public static bool RunTowards(GameEntity gameEntity)
    {
        // Debug.Log("Run Towards function");
        Vector3 distance = gameEntity.targetPosition.value - gameEntity.position.value;
        if (distance.magnitude < 0.00001f) {
            gameEntity.ReplacePosition(gameEntity.targetPosition.value);
            return true;
        }

        Vector3 direction = distance.normalized;
        gameEntity.ReplaceRotation(Quaternion.LookRotation(direction));

        Vector3 targetPosition = gameEntity.targetPosition.value - (direction * gameEntity.distanceToEnemy.value);
        distance = targetPosition - gameEntity.position.value;

        Vector3 step = direction * gameEntity.runSpeed.value;
        if (step.magnitude < distance.magnitude) {
            gameEntity.ReplacePosition(gameEntity.position.value + step);
            return false;
        }

        gameEntity.ReplacePosition(targetPosition);
        return true;
    }
}