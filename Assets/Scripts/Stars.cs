using UnityEngine;

namespace Planet
{
    public class Stars : MonoBehaviour
    {
        [SerializeField] float StarDistance = 5f;
        [SerializeField] float StarCount = 70;
        [SerializeField] bool SpawnOnStart = true;
        [SerializeField] GameObject StarPrefab = null;

        void Start()
        {
            if (SpawnOnStart)
                SpawnStars();
        }

        public void SpawnStars()
        {
            var parent = new GameObject(name + "_stars_parent").transform;
            parent.parent = transform;
            parent.localPosition = Vector3.zero;

            GameObject go;
            for (int i = 0; i < StarCount; i++)
            {
                // TODO: create one mesh instead of objects

                go = Instantiate(StarPrefab, UnityEngine.Random.onUnitSphere * StarDistance * (UnityEngine.Random.value + .5f), Quaternion.identity);
                go.transform.SetParent(parent);
            }
        }
    }
}