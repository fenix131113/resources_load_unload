using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace ResourceSystem
{
    public interface IResourceManager
    {
        T Load<T>(string path, out GUID id) where T : Object;
        Task<AsyncLoadResult<T>> LoadAsync<T>(string path) where T : Object;
        void Unload(GUID id);
    }
}