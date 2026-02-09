using UnityEngine;
using UnityEngine.UI;

namespace Levels.View
{
    public class LoadNextButton : MonoBehaviour
    {
        [SerializeField] private Button loadButton;
        [SerializeField] private LevelLoader levelLoader;
        
        private void Start() => Bind();

        private void OnDestroy() => Expose();

        private void OnButtonClick() => levelLoader.LoadNextLevel();

        private void Bind() => loadButton.onClick.AddListener(OnButtonClick);

        private void Expose() => loadButton.onClick.RemoveAllListeners();
    }
}