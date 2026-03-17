using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace ResourceSystem
{
    public class ResourceManager : IResourceManager
    {
        private readonly IRepository _repository = new AssetsRepository();
        
        public T Load<T>(string path, out GUID id) where T : Object
        {
            if (_repository.IsInRepository(path, out var guid))
            {
                id = guid;
                _repository.GetFromRepository<T>(guid, out var result);
                return result;
            }
            
            id = GUID.Generate();
            var loaded = Resources.Load<T>(path);
            _repository.AddToRepository(new AssetEntry(loaded, path), id);
            return loaded;
        }

        public async Task<AsyncLoadResult<T>> LoadAsync<T>(string path) where T : Object
        {
            GUID id;
            
            if (_repository.IsInRepository(path, out var guid))
            {
                id = guid;
                _repository.GetFromRepository<T>(guid, out var result);
                return new AsyncLoadResult<T>(result, id);
            }
            
            id = GUID.Generate();
            var loaded = await Task.Run(() => Resources.LoadAsync<T>(path));
            _repository.AddToRepository(new AssetEntry(loaded, path), id);
            return new AsyncLoadResult<T>(loaded.asset as T, id);
        }

        public void Unload(GUID id)
        {
            _repository.RemoveFromRepository(id);
        }
    }
}