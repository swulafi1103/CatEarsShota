using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasigoTutorial : MonoBehaviour
{
    public enum Hasiho_Case {
        Up = 0,
        Down
    }

    [SerializeField]
    Hasiho_Case hasigo;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag != "Player") return;

        TutorialContriller.Instance.Hasigo(true, (int)hasigo);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag != "Player") return;

        TutorialContriller.Instance.Hasigo(false, (int)hasigo);
    }
}
