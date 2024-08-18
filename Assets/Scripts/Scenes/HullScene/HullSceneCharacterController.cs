using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullSceneCharacterController : MonoBehaviour
{
    public delegate void CollectHandler(HullSceneCollectible collectible);
    public event CollectHandler onPartCollected;

    public Rigidbody2D characterBody;

    private static float characterSpeed = 6.0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            characterBody.MovePosition(characterBody.position + Vector2.left * Time.fixedDeltaTime * characterSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            characterBody.MovePosition(characterBody.position + Vector2.right * Time.fixedDeltaTime * characterSpeed);
        }       
    }

    public void OnTriggerEnter2D(Collider2D collider) {            
        GameObject triggeredObject = collider.gameObject;
        HullSceneCollectible collectible = triggeredObject.GetComponent<HullSceneCollectible>();
        if (collectible != null) {
            onPartCollected?.Invoke(collectible);
            Destroy(triggeredObject);
        }
    }
}
