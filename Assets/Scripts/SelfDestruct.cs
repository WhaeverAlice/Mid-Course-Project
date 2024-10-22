using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DeleteAfterTime());
    }

    IEnumerator DeleteAfterTime()
    {
        yield return new WaitForSeconds(60f);
        Destroy(this.gameObject);
    }
}
