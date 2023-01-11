using UnityEngine;

public class GameColorsManager : MonoBehaviour
{
    [SerializeField] private Renderer _playerRenderer;

    [Space(10), SerializeField] private Renderer _stageRenderer;

    private MaterialPropertyBlock _propertyBlock;

    private const string propertyBlockColorFieldName = "_Color";

    private void Awake()
    {
        _propertyBlock = new MaterialPropertyBlock();
    }

    public void ChangePlayerColor(Color newColor)
    {
        _playerRenderer.GetPropertyBlock(_propertyBlock);

        _propertyBlock.SetColor(propertyBlockColorFieldName, newColor);

        _playerRenderer.SetPropertyBlock(_propertyBlock);
    }

    public void ChangeStageColor(Color newColor)
    {
        _stageRenderer.GetPropertyBlock(_propertyBlock);

        _propertyBlock.SetColor(propertyBlockColorFieldName, newColor);

        _stageRenderer.SetPropertyBlock(_propertyBlock);
    }
}
