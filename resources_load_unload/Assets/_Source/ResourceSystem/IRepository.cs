using System;
using UnityEditor;

namespace ResourceSystem
{
    public interface IRepository : IDisposable
    {
        bool IsInRepository(GUID guid);
        bool IsInRepository(string path, out GUID guid);
        bool GetFromRepository<T>(GUID guid, out T result);
        bool AddToRepository(AssetEntry entry, GUID guid);
        bool RemoveFromRepository(GUID guid);
    }
}