using UnityEngine;

public class MainMenuParallax : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0f;
    private float backgroundWidthSize;
    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        backgroundWidthSize = sprite.texture.width / sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = moveSpeed * Time.deltaTime;
        transform.position += new Vector3(moveX, 0f);
        if (Mathf.Abs(transform.position.x) > backgroundWidthSize)
        {
            transform.position = new Vector3(0f, transform.position.y);
        }
    }
}
