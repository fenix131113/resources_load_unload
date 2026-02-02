using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ResourceSystem
{
    public class AssetsRepository : IRepository
    {
        private readonly Dictionary<GUID, object> _assets = new();
        private readonly Dictionary<Type, HashSet<object>> _hashAssets = new();

        public bool IsInRepository(object obj)
        {
            if (!_hashAssets.ContainsKey(obj.GetType()))
                return false;

            _hashAssets.TryGetValue(obj.GetType(), out var set);
            return set != null && set.Contains(obj);
        }

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
            if (!_hashAssets.ContainsKey(obj.GetType()))
                _hashAssets.Add(obj.GetType(), new HashSet<object>());

            _assets.TryAdd(guid, obj);

            if (!IsInRepository(obj))
                return true;

            Debug.LogWarning("Trying to add asset to a hash asset that is already in the repository!");
            return false;
        }

        public bool RemoveFromRepository(object obj)
        {
            if (!IsInRepository(obj))
                return false;

            _assets.Remove(_assets.First(x => x.Value == obj).Key);
            _hashAssets[obj.GetType()].Remove(obj);
            return true;
        }

        public bool RemoveFromRepository(GUID guid)
        {
            if (!IsInRepository(guid))
                return false;

            _assets.Remove(guid);
            _hashAssets[_assets[guid].GetType()].Remove(_assets[guid]);
            return true;
        }

        public void Dispose()
        {
            _assets.Clear();
            _hashAssets.Clear();
        }
    }
}