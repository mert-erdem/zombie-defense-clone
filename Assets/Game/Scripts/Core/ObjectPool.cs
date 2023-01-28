using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Core
{
    /// <summary>
    /// T: Actual object type
    /// T1: Pool's type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T1"></typeparam>
    public class ObjectPool<T, T1> : Singleton<ObjectPool<T, T1>> where T : Component where T1 : Component
    {
        [SerializeField] private T poolObject;
        [SerializeField] private int poolSize;
        [SerializeField] private float objectDuration = 1f;
        private List<T> pool;


        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(this.transform.parent);
        }

        private void Start()
        {
            pool = new List<T>();

            for (int i = 0; i < poolSize; i++)
            {
                var objectForPool = Instantiate(poolObject, transform);
                objectForPool.gameObject.SetActive(false);
                pool.Add(objectForPool);
            }
        }

        public T GetObject()
        {
            for (int i = 0; i < poolSize; i++)
            {
                if (!pool[i].gameObject.activeInHierarchy)
                {
                    pool[i].gameObject.SetActive(true);
                    return pool[i];
                }
            }

            return null;
        }

        public void PullObjectBack(T theObject)
        {
            StartCoroutine(PullObjectBackRoutine(theObject));
        }
        private IEnumerator PullObjectBackRoutine(T theObject)
        {
            yield return new WaitForSeconds(objectDuration);

            theObject.transform.rotation = Quaternion.identity;
            theObject.transform.SetParent(this.transform);
            theObject.gameObject.SetActive(false);
        }

        public void PullObjectBackImmediate(T theObject)
        {
            theObject.transform.rotation = Quaternion.identity;
            theObject.transform.SetParent(this.transform);
            theObject.gameObject.SetActive(false);
        }
    }
}