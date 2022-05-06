using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBox : MonoBehaviour
{
    [SerializeField] private GameObject pickupSparkPrefab;
    private void OnTriggerEnter2D(Collider2D other) {
        Instantiate(pickupSparkPrefab, other.transform.position, Quaternion.identity);
        Destroy(other.gameObject);
        Debug.Log("got a key!");
    }
}
