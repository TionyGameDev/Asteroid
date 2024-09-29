using System;
using System.Collections.Generic;
using Ability;
using Assets.Scripts.MVC;
using Events.MessageSystem;
using Events.MessageSystem.Messages;
using GameSystem.GameState;
using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class BaseGameUI : MonoBehaviour, IGameUI
    , IMessageListener<SendNewPosition_Msg>
    , IMessageListener<SendCreatePlayer_Msg>
    , IMessageListener<SendDeadCharacter_Msg>
    , IMessageListener<SendAbility_Msg>
{
    [SerializeField] 
    private TextMeshProUGUI _coordinateText;
    [SerializeField] 
    private TextMeshProUGUI _rotationText;
    [SerializeField] 
    private TextMeshProUGUI _speedText;

    [SerializeField] 
    private GameObject _defeat;

    [SerializeField] 
    private List<BaseUIAbility> _abilities;

    private PlayerStateService _playerStateService;

    private void Awake()
    {
        ((IGameUI)this).Init();
        _playerStateService = new PlayerStateService();
    }

    public void Init()
    {
        MessageBroker.localBus.broadcastChannel.Subscribe(this);
    }

    public void Dispose()
    {
        MessageBroker.localBus.broadcastChannel.Unsubscribe(this);
    }

    private void OnEnable()
    {
        if (_playerStateService != null)
        {
            _playerStateService.OnRotationChanged += DrawRotation;
            _playerStateService.OnSpeedChanged += DrawSpeed;
        }
    }

    private void OnDisable()
    {
        if (_playerStateService != null)
        {
            _playerStateService.OnRotationChanged -= DrawRotation;
            _playerStateService.OnSpeedChanged -= DrawSpeed;
        }
    }

    private void Update()
    {
        _playerStateService?.UpdateState();
    }

    private void DrawRotation(int rotation)
    {
        if (_rotationText)
            _rotationText.text = $"{rotation}%";
    }

    private void DrawSpeed(int speed)
    {
        if (_speedText)
            _speedText.text = $"Speed: {speed}";
    }

    public void OnMessage(SendNewPosition_Msg message)
    {
        if (_coordinateText)
            _coordinateText.text = $"{message.position.x},{message.position.y}";
    }

    public void OnMessage(SendDeadCharacter_Msg message)
    {
        if (message.targetDead.GetComponent<PlayerController>())
        {
            if (_defeat)
            {
                _defeat.gameObject.SetActive(true);
                GameStateMachine.Instance.SetState(new EndGameState());
            }
        }
    }

    public void Restart()
    {
        GameStateMachine.Instance.SetState(new StartGameState());
    }

    public void OnMessage(SendCreatePlayer_Msg message)
    {
        _playerStateService.Init(message.controller);
    }

    public void OnMessage(SendAbility_Msg message)
    {
        for (int i = 0; i < _abilities.Count; i++)
            _abilities[i].Init(message.ability);
    }
}

}