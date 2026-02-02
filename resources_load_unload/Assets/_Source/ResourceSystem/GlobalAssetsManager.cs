using UnityEngine;

namespace ResourceSystem
{
    public static class GlobalAssetsManager
    {
        private static bool _initialized;
        
        public static IResourceManager ResourceManager { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            if (_initialized)
                return;
            
            InitializeInternal();
            _initialized = true;
        }

        private static void InitializeInternal()
        {
            ResourceManager = new ResourceManager();
        }
    }
}