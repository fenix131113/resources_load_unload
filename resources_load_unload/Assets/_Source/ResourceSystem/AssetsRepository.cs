using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ResourceSystem
{
    public class AssetsRepository : IRepository
    {
        private readonly Dictionary<GUID, AssetEntry> _assets = new();

        public bool IsInRepository(GUID guid) => _assets.ContainsKey(guid);

        public bool IsInRepository(string path, out GUID guid)
        {
            foreach (var x in _assets.Where(x => x.Value.Path == path))
            {
                guid = x.Key;
                return true;
            }

            guid = default;
            return false;
        }

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

            _assets[guid].IncreaseRefCount();
            result = (T)_assets[guid].Asset;
            return true;
        }

        public bool AddToRepository(AssetEntry entry, GUID guid)
        {
            if (IsInRepository(guid) || IsInRepository(entry.Path, out _))
            {
                Debug.LogWarning("Trying to add asset with guid that is already in the repository!");
                return false;
            }

            if (!_assets.TryAdd(guid, entry))
                return false;
            
            _assets[guid].IncreaseRefCount();
            return true;

        }

        public bool RemoveFromRepository(GUID guid)
        {
            if (!IsInRepository(guid))
                return false;

            _assets[guid].DecreaseRefCount();

            if (_assets[guid].RefCount > 0)
                return true;
            
            Resources.UnloadAsset(_assets[guid].Asset as Object);
            _assets.Remove(guid);

            return true;
        }

        public void Dispose()
        {
            _assets.Clear();
        }
    }
}