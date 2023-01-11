using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class ColorButton : MonoBehaviour
{
    [SerializeField] private ButtonType _buttonType;

    private static GameManager _gameManager;

    private RectTransform _thisButtonRectTransform;

    private RawImage _checkColorBoxImage;

    private Color _buttonColor;

    private void Awake()
    {
        _thisButtonRectTransform = GetComponent<RectTransform>();

        _buttonColor = GetComponent<Image>().color;

        if (_gameManager == null)
        {
            _gameManager = GameManager.Instance;
        }
    }

    private void Start()
    {
        switch (_buttonType)
        {
            case ButtonType.ManColor:

                GetCheckBoxImage(_gameManager.ColorPanelManager.ManColorCheckBoxImage);

                AddListenerToThisButton(SelectThisButtonAsPlayerColor);

                break;

            case ButtonType.StageColor:

                GetCheckBoxImage(_gameManager.ColorPanelManager.StageColorCheckBoxImage);

                AddListenerToThisButton(SelectThisButtonAsStageColor);

                break;
        }
    }

    private void AddListenerToThisButton(UnityEngine.Events.UnityAction action)
    {
        GetComponent<Button>().onClick.AddListener(action);
    }

    private void GetCheckBoxImage(RawImage checkBoxImage)
    {
        if (_checkColorBoxImage == null)
        {
            _checkColorBoxImage = checkBoxImage;
        }
    }

    private void SelectThisButtonAsPlayerColor()
    {
        _gameManager.GameColors.ChangePlayerColor(_buttonColor);

        SetCheckBoxImagePositionToThisButtonPosition();
    }

    private void SelectThisButtonAsStageColor()
    {
        _gameManager.GameColors.ChangeStageColor(_buttonColor);

        SetCheckBoxImagePositionToThisButtonPosition();
    }

    private void SetCheckBoxImagePositionToThisButtonPosition()
    {
        _checkColorBoxImage.rectTransform.SetParent(_thisButtonRectTransform, false);

        _checkColorBoxImage.rectTransform.localPosition = Vector3.zero;
    }

    private enum ButtonType 
    {
        ManColor,
        StageColor
    }
}
