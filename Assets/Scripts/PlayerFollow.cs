using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 offset = new Vector3(-0.03f, 3.16f,-4.9f);

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;

    }
}
