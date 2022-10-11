using Geniral_Settings;
using UnityEngine;

namespace Factory
{
    public class Factory : MonoBehaviour
    {
        public bool isSettingsIntroducted;
        
        [SerializeField] private GameObject plane;
        [SerializeField] private GameObject cube;

        [SerializeField] private Transform cubeSpawnPosition;
        [SerializeField] private GeniralSettings _geniralSettings;
    
        private float spawnTime;
        private float spawnTimeLast;

        private bool isSpawnPlane;
        private bool isSpawnPlaneComplited;

        private void Awake()
        {
            spawnTime = _geniralSettings.spawnTime;
            spawnTimeLast = spawnTime;
            //Debug.Log(spawnTime);
        }

        private void Update()
        {
            if (isSettingsIntroducted) TimerToSpawnCubes();
            //Debug.Log(spawnTime);

            CheckSpawnPlane();
        }

        private void CheckSpawnPlane()
        {
            if (isSpawnPlane && !isSpawnPlaneComplited)
            {
                SpawnPLane();
                isSpawnPlaneComplited = true;
            }
        }

        private void TimerToSpawnCubes()
        {
            isSpawnPlane = true;
            
            spawnTimeLast -= Time.deltaTime;

            if (spawnTimeLast <= 0)
            {
                SpawnCube();
                spawnTimeLast = spawnTime;
            }
        }

        private void SpawnPLane() =>
            Instantiate(plane, Vector3.zero, Quaternion.identity);
    
        private void SpawnCube() =>
            Instantiate(cube, cubeSpawnPosition.transform.position, Quaternion.identity, transform);
        
    }
}
