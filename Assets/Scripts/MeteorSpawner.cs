using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Planet
{
    public class MeteorSpawner : MonoBehaviour, IStartGameCallback, IGameOverCallback, IHasGameManager
    {
        [SerializeField] GameManager GameManager = null;
        [SerializeField] float SpawnDistance = 2f;
        [SerializeField] float StopDistance = 1f;
        [SerializeField] float MoveSpeed = .01f;
        [SerializeField] GameObject MeteorPrefab = null;

        List<Meteor> Meteors = new List<Meteor>();
        List<Meteor> PlacedMeteors = new List<Meteor>();
        Coroutine SpawnMeteorCoroutineVar;

        GameManager IHasGameManager.GameManager => GameManager;

        void Start()
        {
            GameManager.AddCallback(this as IStartGameCallback);
            GameManager.AddCallback(this as IGameOverCallback);
        }
        void FixedUpdate()
        {
            if (!GameManager.IsAlive) return;

            for (int i = 0; i < Meteors.Count; i++)
            {
                if (Vector3.Distance(Meteors[i].transform.position, transform.position) <= StopDistance + StopDistance / 100f)
                {
                    PlacedMeteors.Add(Meteors[i]);
                    Meteors.RemoveAt(i--);
                }
                else
                {
                    Meteors[i].transform.position = Vector3.MoveTowards(Meteors[i].transform.position, transform.position, MoveSpeed);
                }
            }
        }

        IEnumerator SpawnMeteorCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.value / 2f);
                SpawnMeteor(Random.onUnitSphere * SpawnDistance);
            }
        }
        void SpawnMeteor(Vector3 position)
        {
            // TODO: caching
            var meteor = Instantiate(MeteorPrefab, position, Quaternion.identity);

            Meteors.Add(meteor.GetComponent<Meteor>());
        }

        public void OnStartGame()
        {
            SpawnMeteorCoroutineVar = StartCoroutine(SpawnMeteorCoroutine());
        }
        public void OnGameOver()
        {
            StopCoroutine(SpawnMeteorCoroutineVar);

            foreach (var meteor in Meteors.Concat(PlacedMeteors))
                if (meteor != null)
                    Destroy(meteor.gameObject);

            Meteors.Clear();
            PlacedMeteors.Clear();
        }
    }
}