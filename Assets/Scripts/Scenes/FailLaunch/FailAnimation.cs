using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailAnimation : MonoBehaviour
{
    public void OnDestroyAnimationComplete() {            
        Destroy(gameObject);
    }
}
