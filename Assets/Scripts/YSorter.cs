using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YSorter : MonoBehaviour
{
    public float precision = 100f;
    public int orderOffset = 0;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
 
    void LateUpdate()
    {
        spriteRenderer.sortingOrder = orderOffset + Mathf.RoundToInt(-transform.position.y * precision);
    }
}
