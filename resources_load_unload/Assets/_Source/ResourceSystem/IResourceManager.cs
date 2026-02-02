using UnityEditor;

namespace ResourceSystem
{
    public interface IResourceManager
    {
        T Load<T>(string path, out GUID id);
        void Unload(GUID id);
    }
}