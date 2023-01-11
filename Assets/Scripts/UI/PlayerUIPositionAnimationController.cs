using UnityEngine;
using UnityEngine.UI;

public class PlayerUIPositionAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    [Space(15)]
    [SerializeField] private Animator _playerUIPositionAnimatorController;

    [SerializeField] private Text _playerUIPositionText;

    private const string DefaultPassedPointText = "Passing Point ";

    private const string PlayerUIPositionAnimationName = "PlayerUIPositionAnimation";

    private void Awake()
    {
        _playerMovement.OnPlayerReachedPointPosition += UpdatePointPassedText;

        _playerMovement.OnPlayerReachedPointPosition += PlayPointPassedAnimation;
    }

    private void UpdatePointPassedText()
    {
        _playerUIPositionText.text = DefaultPassedPointText + _playerMovement.CurrentlyStagePoint;
    }

    private void PlayPointPassedAnimation()
    {
        _playerUIPositionAnimatorController.Play(PlayerUIPositionAnimationName, 0, 0f);
    }

    private void OnDestroy()
    {
        if(_playerMovement != null)
        {
            _playerMovement.OnPlayerReachedPointPosition -= UpdatePointPassedText;

            _playerMovement.OnPlayerReachedPointPosition -= PlayPointPassedAnimation;
        }
    }
}
