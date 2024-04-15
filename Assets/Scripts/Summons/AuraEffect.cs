using UnityEngine;

public class AuraEffect : MonoBehaviour
{
    public float minAlpha = 0.3f;
    public float maxAlpha = 0.7f;
    public float pulseSpeed = 1.0f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, Mathf.Sin(Time.time * pulseSpeed) * 0.5f + 0.5f);
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    }
}
