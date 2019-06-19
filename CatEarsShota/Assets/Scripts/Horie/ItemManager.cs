using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //[SerializeField]
    //private List<ItemData> itemList = new List<ItemData>();

    private static ItemManager instance;
    public static ItemManager Instance {
        get { return instance; }
    }

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    /// <summary>
    /// パンツ切り替え
    /// </summary>
    /// <param name="num"></param>
    public void ChangePants(int num) {
        //getpuntsnum(num-12);  //伊豆先輩の関数呼び出す
        Debug.Log(num - 12);
    }
}
