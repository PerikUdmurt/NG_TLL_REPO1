using MyPetProject;
using UnityEngine;
using Zenject;

public class DimensionViewCamera: MonoBehaviour
{
    public LayerMask LightDimVisible;
    public LayerMask DarkDimVisible;

    private Camera _camera;
    [Inject]
    private void Construct(DimensionManager dimensionManager)
    {
        dimensionManager.DimensionSwetched.AddListener(SwitchCameraView);
    }

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void SwitchCameraView(DimensionManager.Dimension dimension)
    {
        if (dimension == DimensionManager.Dimension.PeopleDimension)
        {
            _camera.cullingMask = LightDimVisible;
        }
        else _camera.cullingMask = DarkDimVisible;
    }
}
