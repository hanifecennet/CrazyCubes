using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushForce;
    [SerializeField] private float cubeMaxPosX; //küpün x düzeyinde en fazla alabileceği değer
    [Space]
    [SerializeField] private TouchSlider touchSlider;

    private Cube mainCube;

    private bool isPointerDown;
    private bool canMove;
    private Vector3 cubePos;

    // Start is called before the first frame update
    void Start()
    {
        SpawnCube();
        canMove = true;

        //Slider değere göre hareket etme:
        touchSlider.OnPointerDownEvent += OnPointerDown;
        touchSlider.OnPointerDragEvent += OnPointerDrag;
        touchSlider.OnPointerUpEvent += OnPointerUp;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPointerDown)
            mainCube.transform.position = Vector3.Lerp(
                          mainCube.transform.position,
                          cubePos,
                          moveSpeed * Time.deltaTime
                          );
    }

    private void OnPointerDown()
    {
        isPointerDown = true;
    }

    private void OnPointerDrag(float xMovement)
    {
        if (isPointerDown)
        {
            cubePos = mainCube.transform.position;
            cubePos.x = xMovement * cubeMaxPosX;
        }
    }

    private void OnPointerUp()
    {
        if (isPointerDown && canMove)
        {
            isPointerDown = false;
            canMove = false;

            //Küpü itmek için:
            mainCube.CubeRigidbody.AddForce(Vector3.forward * pushForce, ForceMode.Impulse);

            //0,3 saniye sonra yeni küp oluşturmak için:
            Invoke("SpawnNewCube", 0.3f);
        }
    }

    private void SpawnNewCube()
    {
        mainCube.IsMainCube = false;
        canMove = true;
        SpawnCube();
    }

    private void SpawnCube()
    {
        mainCube = CubeSpawner.Instance.SpawnRandom();
        mainCube.IsMainCube = true;
        //cubePos değerini sıfırlamak için:
        cubePos = mainCube.transform.position;
    }

    private void OnDestroy()
    {
        touchSlider.OnPointerDownEvent -= OnPointerDown;
        touchSlider.OnPointerDragEvent -= OnPointerDrag;
        touchSlider.OnPointerUpEvent -= OnPointerUp;
    }
}
