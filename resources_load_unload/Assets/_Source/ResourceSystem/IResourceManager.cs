using UnityEditor;
using UnityEngine;

namespace ResourceSystem
{
    public interface IResourceManager
    {
        T Load<T>(string path, out GUID id) where T : Object;
        void Unload(GUID id);
    }
}