using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CoroutineScope
{
    void launch(IEnumerator routine);
}
