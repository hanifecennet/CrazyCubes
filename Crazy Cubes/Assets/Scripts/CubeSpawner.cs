using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    // Singleton class
    public static CubeSpawner Instance;

    Queue <Cube> cubesQueue = new Queue <Cube> () ;
    [SerializeField] private int cubesQueueCapacity = 20;
    [SerializeField] private bool autoQueueGrow = true;

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Color[] cubeColors;

    [HideInInspector] public int maxCubeNumber; // bu levelde max küpümüz 4096 (2^12)
    private int maxPower = 12;

    private Vector3 defaultSpawnPosition;

    private void Awake ()
    {
        Instance = this;

        defaultSpawnPosition = transform.position;
        maxCubeNumber = (int) Mathf.Pow(2, maxPower);

        InitializeCubeQueue();
    }

    private void InitializeCubeQueue()
    {
        for (int i=0; i<cubesQueueCapacity; i++)
        {
            AddCubeToQueue();
        }
    }

    private void AddCubeToQueue()
    {
       Cube cube = Instantiate(cubePrefab, defaultSpawnPosition, Quaternion.identity, transform)
                              .GetComponent<Cube>();
        cube.gameObject.SetActive(false);
        cube.IsMainCube = false;
        cubesQueue.Enqueue(cube);
    }

    public Cube Spawn(int number, Vector3 position)
    {
        if (cubesQueue.Count == 0)
        {
            if (autoQueueGrow)
            {
                cubesQueueCapacity++;
                AddCubeToQueue();
            }
            else
            {
                Debug.LogError("[Küp Sırası] : havuzda daha fazla küp yok]");
                return null;
            }
        }

        Cube cube = cubesQueue.Dequeue();
        cube.transform.position = position;
        cube.SetNumber(number);
        cube.SetColor(GetColor(number));
        cube.gameObject.SetActive(true);

        return cube;
    }

    public Cube SpawnRandom()
    {
        return Spawn(GenerateRandomNumber(), defaultSpawnPosition);
    }

    public void DestroyCube(Cube cube)
    {
        cube.CubeRigidbody.velocity = Vector3.zero;
        cube.CubeRigidbody.angularVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;
        cube.IsMainCube = false;
        cube.gameObject.SetActive(false);
        cubesQueue.Enqueue(cube);
    }

    public int GenerateRandomNumber ()
    {
        return (int)Mathf.Pow(2, Random.Range(1, 6)); //2,4,8,16,32 rastgele küpler oluşturulsun
    }

    private Color GetColor(int number)
    {
        return cubeColors[(int)(Mathf.Log(number) / Mathf.Log(2)) - 1];
        //2^3 = 8 --> 3'ü elde edebilmek için --> log(8)/log(2) = 3
        //-1 index 0 ile başlar

    }
}
