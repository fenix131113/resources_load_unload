using Levels;
using ResourceSystem;
using UnityEngine;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private LevelLoader levelLoader;

        private readonly IResourceManager _resourceManager = new ResourceManager();
        
        private void Awake()
        {
            levelLoader.Init(_resourceManager);
        }
    }
}