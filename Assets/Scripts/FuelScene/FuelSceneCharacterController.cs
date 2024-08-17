using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSceneCharacterController : MonoBehaviour
{
    public delegate void OpenDoorHandler(FuelStoreDoor door);
    public event OpenDoorHandler onDoorOpened;

    private static float characterSpeed = 3.0f;

    public Rigidbody2D characterBody;

    private FuelStoreDoor collidedDoor;

    public void activate() {
        gameObject.SetActive(true);
    }

    public void deactivate() {
        gameObject.SetActive(false);
    }

    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow)) {
            characterBody.AddForce(Vector2.up * 50.0f);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            characterBody.MovePosition(characterBody.position + Vector2.left * Time.fixedDeltaTime * characterSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            characterBody.MovePosition(characterBody.position + Vector2.right * Time.fixedDeltaTime * characterSpeed);
        }
        if (Input.GetKey(KeyCode.E) && collidedDoor != null){
            onDoorOpened?.Invoke(collidedDoor);
            collidedDoor = null;
        }
    }

    public void OnTriggerEnter2D(Collider2D collider) {        
        GameObject triggeredObject = collider.gameObject;
        FuelStoreDoor door = triggeredObject.GetComponent<FuelStoreDoor>();
        if (door != null) {
            collidedDoor = door;
        }
    }

    public void OnTriggerExit2D(Collider2D collider) {        
        GameObject triggeredObject = collider.gameObject;        
        FuelStoreDoor door = triggeredObject.GetComponent<FuelStoreDoor>();
        if (door == collidedDoor) {
            collidedDoor = null;
        }
    }
}
