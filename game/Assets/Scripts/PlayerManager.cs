using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;

    void Start()
    {
        instance = this;
    }
    #endregion

    public GameObject player;

}
