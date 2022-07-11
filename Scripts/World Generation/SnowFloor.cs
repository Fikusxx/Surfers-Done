using UnityEngine;


public class SnowFloor : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Material material;


    private void Update()
    {
        transform.position = new Vector3(0, 0, player.transform.position.z);
    }



}
