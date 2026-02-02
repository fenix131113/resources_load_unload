using UnityEditor;

namespace ResourceSystem
{
    public class ResourceManager : IResourceManager
    {
        private IRepository _repository = new AssetsRepository();
        
        public T Load<T>(string path, out GUID id)
        {
            throw new System.NotImplementedException();
        }

        public void Unload(GUID id)
        {
            throw new System.NotImplementedException();
        }
    }
}