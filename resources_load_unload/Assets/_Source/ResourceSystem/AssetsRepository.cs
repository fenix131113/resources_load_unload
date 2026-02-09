using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ResourceSystem
{
    public class AssetsRepository : IRepository
    {
        private readonly Dictionary<GUID, object> _assets = new();

        public bool IsInRepository(GUID guid) => _assets.ContainsKey(guid);

        public bool GetFromRepository<T>(GUID guid, out T result)
        {
            if (!IsInRepository(guid))
            {
                result = default;
                return false;
            }

            if (_assets[guid] is not T)
            {
                Debug.LogError($"Type miss match assets with guid: {guid.ToString()}");
                result = default;
                return false;
            }
            
            result = (T)_assets[guid];
            return true;
        }

        public bool AddToRepository(object obj, GUID guid)
        {
            if (IsInRepository(guid))
            {
                Debug.LogWarning("Trying to add asset with guid that is already in the repository!");
                return false;
            }
            
            _assets.TryAdd(guid, obj);

            return true;
        }

        public bool RemoveFromRepository(GUID guid)
        {
            if (!IsInRepository(guid))
                return false;

            _assets.Remove(guid);
            return true;
        }

        public void Dispose()
        {
            _assets.Clear();
        }
    }
}