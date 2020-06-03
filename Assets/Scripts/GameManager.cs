using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private ArrayList Players;
    [SerializeField]
    private GameObject player1;
    private int maxPlayerCount = 4;
    private ArrayList joystickAlreadyAdd;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
        }
        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Players = new ArrayList();
        Players.Add(player1);
        joystickAlreadyAdd = new ArrayList();
        joystickAlreadyAdd.Add(1);
        //player1 = GameObject.Find("Player1");
    }

    // Update is called once per frame
    void Update()
    {
        if(Players.Count < maxPlayerCount)
        {
            for (int i = 1; i <= 16; i++)
            {
                if (Input.GetButton("Submit_Joystick" + i) && joystickAlreadyAdd.IndexOf(i, 0) == -1)
                {
                    GameObject newPlayer;
                    int newplayerNumber = Players.Count + 1;
                    newPlayer = Instantiate(player1);
                    newPlayer.transform.name = "Player" + (newplayerNumber);
                    newPlayer.transform.SetPositionAndRotation(new Vector3(newPlayer.transform.position.x, newPlayer.transform.position.y, newPlayer.transform.position.z + 4.0f), newPlayer.transform.rotation);
                    newPlayer.GetComponent<FisrtPersonCharacterController>().joystickNumber = i;//The player number can be diferent than joysticknumber
                    newPlayer.GetComponent<FisrtPersonCharacterController>().playerNumber = newplayerNumber;
                    Players.Add(newPlayer);
                    joystickAlreadyAdd.Add(i);
                    Debug.Log("Input joystik added: " + i + " -- Player number: " + newplayerNumber);
                    SetPlayerCamerasByPlayerCount(Players.Count);
                    break;
                }
            }

        }
    }

    void SetPlayerCamerasByPlayerCount(int playerCount)
    {
        Camera mainCamera1;
        Camera mainCamera2;
        Camera mainCamera3;
        Camera mainCamera4;

        switch (playerCount)
        {
            case 2:
                mainCamera2 = ((GameObject)Players[1]).GetComponentInChildren<Camera>();
                mainCamera2.rect = new Rect(0f, 0f, 1.0f, 0.5f);
                mainCamera1 = ((GameObject)Players[0]).GetComponentInChildren<Camera>();
                mainCamera1.rect = new Rect(0f, 0.5f, 1.0f, 0.5f);
                break;
            case 3:
                mainCamera2 = ((GameObject)Players[1]).GetComponentInChildren<Camera>();
                mainCamera2.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
                mainCamera3 = ((GameObject)Players[2]).GetComponentInChildren<Camera>();
                mainCamera3.rect = new Rect(0f, 0f, 0.5f, 0.5f);
                break;
            case 4:
                mainCamera1 = ((GameObject)Players[0]).GetComponentInChildren<Camera>();
                mainCamera1.rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
                mainCamera2 = ((GameObject)Players[1]).GetComponentInChildren<Camera>();
                mainCamera2.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                mainCamera4 = ((GameObject)Players[3]).GetComponentInChildren<Camera>();
                mainCamera4.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
                break;
        }
    }

    public void HitManager(GameObject killerPlayer, string objetiveName)
    {
        GameObject objetiveNameGameObject = GameObject.Find(objetiveName);
        FisrtPersonCharacterController fpcController = objetiveNameGameObject.GetComponent<FisrtPersonCharacterController>();

        if (fpcController != null)
        {
            fpcController.health -= 10;

            if(fpcController.health <= 0)
            {
                objetiveNameGameObject.SetActive(false);
            }
        }
    }
}
