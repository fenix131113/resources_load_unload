using System;
using UnityEditor;

namespace ResourceSystem
{
    public interface IRepository : IDisposable
    {
        bool IsInRepository(GUID guid);
        bool GetFromRepository<T>(GUID guid, out T result);
        bool AddToRepository(object obj, GUID guid);
        bool RemoveFromRepository(GUID guid);
    }
}