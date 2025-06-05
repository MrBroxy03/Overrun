using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public GameObject explosion;
    public float healthPoints = 3;

    public Color hurtColor = Color.red;
    public float flashduration = 0.2f;

    public Renderer[] render;
    private Color[][] originalColors;
    private Coroutine flashCoroutine;

    public AudioClip explosionSound;
    void Start()
    {
       render = GetComponentsInChildren<Renderer>();
       originalColors = new Color[render.Length][];
       
       for (int i = 0; i < render.Length; i++)
        {
            Material[] mats = render[i].materials;
            originalColors[i] = new Color[mats.Length];
            for (int j = 0; j < mats.Length; j++)
            {
                mats[j] = new Material(mats[j]);
                originalColors[i][j] = mats[j].color;
            }

            render[i].materials = mats;
        }
    }

    public void TakeDamage()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        healthPoints = healthPoints - 1;

        flashCoroutine = StartCoroutine(FlashEnemy());
    }
    private IEnumerator FlashEnemy()
    {
      foreach (var rend in render)
        {
            foreach (var mat in rend.materials)
            {
                mat.color = hurtColor;
            }
        }
        yield return new WaitForSeconds(flashduration);

       for (int i = 0;i < render.Length; i++)
        {
            Material[] mats = render[i].materials;
            for (int j = 0;j < mats.Length; j++)
            {
                mats[j].color = originalColors[i][j];
            }
        }
    }
    void Update()
    {
        if (healthPoints <= 0)
        {
            SoundEffects.instance.PlaySFXClip(explosionSound, this.transform);
            Instantiate(explosion, this.transform.position, this.transform.rotation);
            var killEnemy = GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemyManager>();
            killEnemy.RemoveEnemy(gameObject);
            
            MaskMeter.meter = Mathf.Clamp(MaskMeter.meter+30,0,300);
            Destroy(gameObject);   
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("PlayerHitbox"))
        {
            TakeDamage();
        }
    }
}
