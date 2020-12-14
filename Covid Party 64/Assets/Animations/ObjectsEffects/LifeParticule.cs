using UnityEngine;

public class LifeParticule : MonoBehaviour
{
    public static LifeParticule Instance;

    public ParticleSystem lifeEffect;
    

    void Awake()
    {
        // On garde une référence du singleton
        if (Instance != null)
        {
            Debug.Log("Multiple instances of LifeParticule!");
            return;
        }

        Instance = this;

    }

    /// <summary>
    /// Création d'une explosion à l'endroit indiqué
    /// </summary>
    /// <param name="position"></param>
    public void Life(Vector3 position)
    {
        
        instantiate(lifeEffect, position);

       
        
    }



    /// <summary>
    /// Création d'un effet de particule depuis un prefab
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(
          prefab,
          position,
          Quaternion.identity
        ) as ParticleSystem;

        // Destruction programmée
        Destroy(
          newParticleSystem.gameObject,
          3f
        );

        return newParticleSystem;
    }
}
