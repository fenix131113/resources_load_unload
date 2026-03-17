using UnityEditor;

namespace ResourceSystem
{
    public class AsyncLoadResult<T>
    {
        public T Asset;
        public GUID Id;

        public AsyncLoadResult(T asset, GUID id)
        {
            Asset = asset;
            Id = id;
        }
    }
}