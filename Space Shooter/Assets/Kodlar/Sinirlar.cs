using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sinirlar : MonoBehaviour { 

    void OnTriggerExit(Collider col) //ateş edilen lazerler bu sinirdan çıktıktan sonra yok olur.(amaç, fps düşüşü ve kasmaları önlemek)
    {
        Destroy(col.gameObject);
    }
}
