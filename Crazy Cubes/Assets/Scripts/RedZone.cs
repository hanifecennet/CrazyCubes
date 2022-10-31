using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedZone : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        Cube cube = other.GetComponent<Cube>();
        if (cube != null)
        {
            if (!cube.IsMainCube && cube.CubeRigidbody.velocity.magnitude < .1f)
            {
                Debug.Log("Gameover");
                SceneManager.LoadScene(2);
            }
        }
    }
}
