using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public GameObject playerprefab;
    CharacterScript cscript;
    Vector2 startpos = new Vector2(0, 0);
    // Start is called before the first frame update
    private void Awake()
    {
        if (playerprefab !=null)
        {
            cscript = GameObject.Instantiate(playerprefab, GameManager.instance.spawnPoints[0].transform.position,transform.rotation).GetComponent<CharacterScript>();
            transform.parent = cscript.transform;
            //cscript = playerprefab.GetComponent<CharacterScript>();
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        cscript.OnMove(context);
    }
}
