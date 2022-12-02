using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMover : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Area")
        {
            Vector2 playerPosition = GameManager.instance.player.transform.position;
            Vector2 position = transform.position;
            float posX = Mathf.Abs(playerPosition.x - position.x);
            float posY = Mathf.Abs(playerPosition.y - position.y);
            float dirX = playerPosition.x > position.x ? 1 : -1;
            float dirY = playerPosition.y > position.y ? 1 : -1;

            if (posX > posY)
            {
                transform.Translate(Vector2.right * 40 * dirX);
            }

            else if (posX < posY) 
            {
                transform.Translate(Vector2.up * 40 * dirY);
            }
        }
    }
}
