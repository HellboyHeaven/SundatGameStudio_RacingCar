using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class GameUI : MonoBehaviour
{
    [SerializeField] private CarController carController;
    private Button _upButton;
    private Button _downButton;
    private Button _leftButton;
    private Button _rightButton;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        _upButton = uiDocument.rootVisualElement.Q<Button>("UpButton");
        _downButton = uiDocument.rootVisualElement.Q<Button>("DownButton");
        _leftButton = uiDocument.rootVisualElement.Q<Button>("LeftButton");
        _rightButton = uiDocument.rootVisualElement.Q<Button>("RightButton");

        _upButton.RegisterCallback<PointerDownEvent>(OnUpButton, TrickleDown.TrickleDown);
        _downButton.RegisterCallback<PointerDownEvent>(OnDownButton, TrickleDown.TrickleDown);
        _leftButton.RegisterCallback<PointerDownEvent>(OnLeftButton, TrickleDown.TrickleDown);
        _rightButton.RegisterCallback<PointerDownEvent>(OnRightButton, TrickleDown.TrickleDown);
        _upButton.RegisterCallback<PointerUpEvent>(OnUpButton);
        _downButton.RegisterCallback<PointerUpEvent>(OnDownButton);
        _leftButton.RegisterCallback<PointerUpEvent>(OnLeftButton);
        _rightButton.RegisterCallback<PointerUpEvent>(OnRightButton);
    }

    private void OnUpButton<T>(T evt) where T : IPointerEvent => carController.VerticalInput = evt is PointerDownEvent ? 1 : 0;
    private void OnDownButton<T>(T evt) where T : IPointerEvent => carController.VerticalInput = evt is PointerDownEvent ? -1 : 0;
    private void OnLeftButton<T>(T evt) where T : IPointerEvent => carController.HorizontalInput = evt is PointerDownEvent ? -1 : 0;
    private void OnRightButton<T>(T evt) where T : IPointerEvent => carController.HorizontalInput = evt is PointerDownEvent ? 1 : 0;
}
