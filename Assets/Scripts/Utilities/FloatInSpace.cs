using UnityEngine;

public class FloatInSpace : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        float moveX = GameManager.Instance.worldSpeed * PlayerController.Instance.boost * Time.deltaTime;
        transform.position += new Vector3(moveX, 0);
        if (transform.position.x < -11f)
        {
            Destroy(gameObject);
        }
    }
}
