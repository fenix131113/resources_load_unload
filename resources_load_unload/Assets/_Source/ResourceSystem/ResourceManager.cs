using UnityEditor;
using UnityEngine;

namespace ResourceSystem
{
    public class ResourceManager : IResourceManager
    {
        private readonly IRepository _repository = new AssetsRepository();
        
        public T Load<T>(string path, out GUID id) where T : Object
        {
            id = GUID.Generate();
            var loaded = Resources.Load<T>(path);
            _repository.AddToRepository(loaded, id);
            return loaded;
        }

        public void Unload(GUID id)
        {
            _repository.RemoveFromRepository(id);
        }
    }
}