using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    [SerializeField]
    GameObject MinigameMgr;
    [SerializeField]
    GameObject playermoves;

   public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "MiniGames")
        {

            if (Input.GetKeyDown(KeyCode.A))//調べ
            {

                playermoves.GetComponent<PlayerMoves>().Notmoves = true;
                MinigameMgr.GetComponent<MiniGameManager>().TouchGenerator();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                MinigameMgr.GetComponent<MiniGameManager>().StartMiniGame();
            }
        }
    }
}
