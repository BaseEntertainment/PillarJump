using System.Collections.Generic;
using UnityEngine;

namespace MagicArsenal
{
    public class MagicEffectCycler : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> listOfEffects;

        [Header("Loop length in seconds")]
        [SerializeField]
        private float loopTimeLength = 5f;

        private float timeOfLastInstantiate;

        private GameObject instantiatedEffect;

        private int effectIndex = 0;

        // Use this for initialization
        private void Start()
        {
            instantiatedEffect = Instantiate(listOfEffects[effectIndex], transform.position, transform.rotation) as GameObject;
            effectIndex++;
            timeOfLastInstantiate = Time.time;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Time.time >= timeOfLastInstantiate + loopTimeLength)
            {
                Destroy(instantiatedEffect);
                instantiatedEffect = Instantiate(listOfEffects[effectIndex], transform.position, transform.rotation) as GameObject;
                timeOfLastInstantiate = Time.time;
                if (effectIndex < listOfEffects.Count - 1)
                    effectIndex++;
                else
                    effectIndex = 0;
            }
        }
    }
}