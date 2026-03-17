namespace ResourceSystem
{
    public class AssetEntry
    {
        public readonly object Asset;
        public readonly string Path;
        public int RefCount;

        public AssetEntry(object asset, string path)
        {
            Asset = asset;
            Path = path;
        }
        
        public void IncreaseRefCount() =>  RefCount++;
        public void DecreaseRefCount() =>  RefCount--;
    }
}