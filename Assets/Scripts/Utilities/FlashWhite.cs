using System.Collections;
using UnityEngine;

public class FlashWhite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Material defaultMaterial;
    private Material flashMaterial;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
        flashMaterial = Resources.Load<Material>("Materials/mWhite"); // Ensure this path matches your project structure
    }

    public void Flash()
    {
        spriteRenderer.material = flashMaterial;
        StartCoroutine("ResetMaterial");
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.2f); // Flash duration
        spriteRenderer.material = defaultMaterial;
    }
}
