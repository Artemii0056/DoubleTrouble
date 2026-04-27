using UnityEngine;

namespace ResourceLoaders.Scripts
{
    public class ResourceLoader : IResourceLoader
    {
        public T Load<T>(string path) where T : Object =>
            Resources.Load<T>(path);

        public T LoadScriptableObject<T>(string path) where T : ScriptableObject =>
            Resources.Load<T>(path);

        public T[] LoadAll<T>(string path) where T : ScriptableObject =>
            Resources.LoadAll<T>(path);
    }
}