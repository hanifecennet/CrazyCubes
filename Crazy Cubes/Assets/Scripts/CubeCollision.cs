using UnityEngine ;

public class CubeCollision : MonoBehaviour 
{
    Cube cube ;
    [SerializeField] private ParticleSystem cubeExplosionFX;
    private int Toplam=0;


    private void Awake () 
    {
      cube = GetComponent<Cube> () ;
    }

   private void OnCollisionEnter (Collision collision) 
   {
      Cube otherCube = collision.gameObject.GetComponent <Cube> () ;

        // diğer küp ile temas edip etmediğini kontrol etmek için:
        if (otherCube != null && cube.CubeID > otherCube.CubeID) 
      {
         // her iki küpün de aynı sayıya sahip olup olmadığını kontrol etmek için:
         if (cube.CubeNumber == otherCube.CubeNumber) 
         {
                Debug.Log("HIT : " + cube.CubeNumber);
                                
                Vector3 contactPoint = collision.contacts [ 0 ].point ;

                // CubeSpawner'da küp sayısının maksimum sayıdan az olup olmadığını kontrol etmek için:
                if (otherCube.CubeNumber < CubeSpawner.Instance.maxCubeNumber) 
                {
                    // sonuç olarak yeni bir küp oluşturmak için:
                    Cube newCube = CubeSpawner.Instance.Spawn (cube.CubeNumber * 2, contactPoint + Vector3.up * 1.6f) ;
                    // yeni küpü yukarı ve ileri hareket etmesi için:
                    float pushForce = 2.5f ;
                    newCube.CubeRigidbody.AddForce (new Vector3 (0, .3f, 1f) * pushForce, ForceMode.Impulse) ;

                    // biraz dönürme/sallama eklemek için:
                    float randomValue = Random.Range (-20f, 20f) ;
                    Vector3 randomDirection = Vector3.one * randomValue ;
                    newCube.CubeRigidbody.AddTorque (randomDirection) ;
                }

            // patlama, çevrelenmiş küpleri de etkilemeleri için:
            Collider[] surroundedCubes = Physics.OverlapSphere (contactPoint, 2f) ;
            float explosionForce = 400f ;
            float explosionRadius = 1.5f ;

            foreach (Collider coll in surroundedCubes) 
            {
               if (coll.attachedRigidbody != null)
                  coll.attachedRigidbody.AddExplosionForce (explosionForce, contactPoint, explosionRadius) ;
            }
             
            // FX
            FX.Instance.PlayCubeExplosionFX (contactPoint, cube.CubeColor) ;
                cubeExplosionFX.Play();
                Debug.Log("effekt");
           
            // İki küpü yok etmek için:
            CubeSpawner.Instance.DestroyCube (cube) ;
            CubeSpawner.Instance.DestroyCube (otherCube) ;

            // Toplama İşlemi
            Toplam += cube.CubeNumber;
            Debug.Log("TOPLAM : " + Toplam);
            }
      }
   }
}
