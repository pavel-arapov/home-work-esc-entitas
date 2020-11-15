using System;
using System.Collections;
using System.Collections.Generic;
using Entitas;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Button attackButton;
    [SerializeField] private Button switchButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button gameOverRestartButton;

    private InputEntity _attackEntity;
    private InputEntity _switchEntity;

    private GameStateEntity _gameStateEntity;

    private Systems _systems;

    private void Awake() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        gameOverCanvas.enabled = false;
        
        var contexts = Contexts.sharedInstance;
        if (!contexts.gameState.isGameState) {
            _attackEntity = contexts.input.CreateEntity();
            _attackEntity.isAttackInput = true;
            _switchEntity = contexts.input.CreateEntity();
            _switchEntity.isSwitchInput = true;
            
            _gameStateEntity = contexts.gameState.CreateEntity();
            _gameStateEntity.isGameState = true;
            _gameStateEntity.AddGameOverScreen(gameOverCanvas);
            _gameStateEntity.AddGameOverText(gameOverText);
        }

        _systems = new Systems();
        _systems.Add(new InputSystem(contexts));
        _systems.Add(new PrefabInstantiateSystem(contexts));
        _systems.Add(new TransformApplySystem(contexts));
        _systems.Add(new AttackSystem(contexts));
        _systems.Add(new ComputeDamageSystem(contexts));
        _systems.Add(new ApplyDamageSystem(contexts));
        _systems.Add(new RunningSystem(contexts));
        _systems.Add(new EndGameSystem(contexts));
        _systems.Add(new AITurnSystem(contexts));
        _systems.Add(new ViewDestroySystem(contexts));
        _systems.Initialize();
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) {
        _gameStateEntity.isWaitingPlayerState = true;
    }

    private void Start() {
        attackButton.onClick.AddListener(OnAttackClick);
        switchButton.onClick.AddListener(OnSwitchClick);
        restartButton.onClick.AddListener(RestartGame);
        gameOverRestartButton.onClick.AddListener(RestartGame);
    }

    private void RestartGame() {
        Contexts.sharedInstance.Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnSwitchClick() {
        _switchEntity.isClick = true;
    }

    private void OnAttackClick() {
        _attackEntity.isClick = true;
    }


    void OnDestroy() {
        Debug.Log("Destroy controller");
        _systems.TearDown();
        attackButton.onClick.RemoveListener(OnAttackClick);
        switchButton.onClick.RemoveListener(OnSwitchClick);
        restartButton.onClick.RemoveListener(RestartGame);
        gameOverRestartButton.onClick.RemoveListener(RestartGame);
    }

    void Update() {
        _systems.Execute();
        _systems.Cleanup();
    }
}