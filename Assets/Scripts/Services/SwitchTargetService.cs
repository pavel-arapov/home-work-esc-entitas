using System;
using Entitas;
using UnityEngine;

public static class SwitchTargetService
{
    public static void SwitchTarget(IGroup<GameEntity> _enemies) {
        GameEntity[] enemies = _enemies.GetEntities();
        GameEntity currentTarget = AttackTargetService.FindCurrentTarget(_enemies);
        if (currentTarget != null) {
            int index = Array.IndexOf(enemies, currentTarget);
            for (int i = 1; i < enemies.Length; i++) {
                int next = (index + i) % enemies.Length;
                if (!enemies[next].isStateDead) {
                    enemies[index].isTarget = false;
                    enemies[next].isTarget = true;
                    enemies[index].targetIndicator.indicator.SetActive(false);
                    // if (enemies[index].view.gameObject.TryGetComponent<Character>(out Character character)) {
                        // character.targetIndicator.gameObject.SetActive(false);                        
                    // }
                    enemies[next].targetIndicator.indicator.SetActive(true);
                    // if (enemies[next].view.gameObject.TryGetComponent<Character>(out Character currentCharacter)) {
                        // currentCharacter.targetIndicator.gameObject.SetActive(true);                        
                    // }
                    return;
                }
            }
        }
        else {
            if (_enemies.count > 0) {
                currentTarget = AttackTargetService.FindAliveAttacker(_enemies);
                currentTarget.isTarget = true;
                if (!currentTarget.hasView) {
                    Debug.Log("No view found...");
                }
                currentTarget.targetIndicator.indicator.SetActive(true);
            }
        }
    }
}