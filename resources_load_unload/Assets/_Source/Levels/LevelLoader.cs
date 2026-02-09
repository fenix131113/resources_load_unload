using ResourceSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private string levelPrefabPath;
        [SerializeField] private int nextSceneIndex;
        
        private IResourceManager _resourceManager;
        private GUID _loadedGuid;
        private GameObject _spawnedLevel;

        public void Init(IResourceManager resourceManager) => _resourceManager = resourceManager;

        private void Start()
        {
            Load();
        }

        public void LoadNextLevel()
        {
            Unload();
            SceneManager.LoadScene(nextSceneIndex);
        }

        private void Load()
        {
            var loaded = _resourceManager.Load<GameObject>(levelPrefabPath, out var guid);
            _spawnedLevel = Instantiate(loaded, Vector3.zero, loaded.transform.rotation);
            _loadedGuid = guid;
        }

        private void Unload()
        {
            _spawnedLevel?.gameObject.SetActive(false);
            _resourceManager.Unload(_loadedGuid);
        }
    }
}