using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class mult : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(SteamFriends.GetPersonaName() + "user id");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
