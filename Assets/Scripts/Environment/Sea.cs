using UnityEngine;

public class Sea : MonoBehaviour
{
    [Range(0.01f, 5f), SerializeField] private float _seaMovingSpeed;

    private Material _seaMaterial;

    private void Awake()
    {
        _seaMaterial = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        _seaMaterial.mainTextureOffset += _seaMovingSpeed * Time.deltaTime * Vector2.one;
    }
}
