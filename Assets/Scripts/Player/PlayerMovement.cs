using System;
using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerMovement : MonoBehaviour
{
    [Range(1f, 20f), SerializeField] private float _playerMovementSpeed;

    [Space(5f), SerializeField] private Transform[] _stagePoints;

    private Animator _playerAnimator;

    private GameManager _gameManager;

    private readonly WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();

    private float _defaultPlayerMovementSpeed;

    public event Action OnPlayerReachedPointPosition;

    public int CurrentlyStagePoint { get; private set; } = 1;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();

        _defaultPlayerMovementSpeed = _playerMovementSpeed;
    }

    private IEnumerator Start()
    {
        _gameManager = GameManager.Instance;

        _gameManager.OnGameStarted += EnablePlayerAnimator;

        _gameManager.OnGameResumed += AllowPlayerMove;

        _gameManager.OnGamePaused += StopMovingPlayer;

        yield return _waitForEndOfFrame;

        _playerAnimator.enabled = false;
    }

    private void EnablePlayerAnimator()
    {
        _playerAnimator.enabled = true;
    }

    private void AllowPlayerMove()
    {
        _playerMovementSpeed = _defaultPlayerMovementSpeed;

        StartCoroutine(MovePlayerTowardsNextPosition());
    }

    private IEnumerator MovePlayerTowardsNextPosition()
    {
        RotatePlayerTowardsNextPosition();

        while (transform.position != _stagePoints[CurrentlyStagePoint].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _stagePoints[CurrentlyStagePoint].position, _playerMovementSpeed * Time.deltaTime);

            yield return _waitForEndOfFrame;
        }

        OnPlayerReachedPointPosition?.Invoke();

        CheckNextStage();
    }

    private void RotatePlayerTowardsNextPosition()
    {
        transform.forward = _stagePoints[CurrentlyStagePoint].position - transform.position;
    }

    private void CheckNextStage()
    {
        CurrentlyStagePoint++;

        if (CurrentlyStagePoint >= _stagePoints.Length)
        {
            CurrentlyStagePoint = 0;
        }

        StartCoroutine(MovePlayerTowardsNextPosition());
    }

    private void StopMovingPlayer()
    {
        StopAllCoroutines();

        _playerMovementSpeed = 0f;
    }

    private void OnDestroy()
    {
        if(_gameManager != null)
        {
            _gameManager.OnGamePaused -= StopMovingPlayer;

            _gameManager.OnGameResumed -= AllowPlayerMove;

            _gameManager.OnGameStarted -= EnablePlayerAnimator;
        }
    }
}
