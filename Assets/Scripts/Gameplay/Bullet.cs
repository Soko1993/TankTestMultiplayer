using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public GameObject target;
    public Vector3 target_pos;
    public float speed;
    public bool PlayerBullet;

    private void Update()
    {
        if (target_pos != null)
        {
            speed = 20f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target_pos, speed);

            if (Vector3.Distance(transform.position, target_pos) <= 0)
            {
                Destroy(gameObject, 0.1f);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (PlayerBullet)
        {
            if (other.GetComponent<Bot_Controller>())
            {
                Debug.Log($"Bullet attack: {other.name} with damage: {damage}");
                other.GetComponent<CharacterEntity>().ChangeCurHP(damage * -1);
                //Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.GetComponent<PlayerController>())
            {
                Debug.Log($"Bullet attack: {other.name} with damage: {damage}");
                other.GetComponent<CharacterEntity>().ChangeCurHP(damage * -1);
                //Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }

        if (other.transform.gameObject.layer == 9)
        {
            //Destroy(gameObject);
        }
    }
}
