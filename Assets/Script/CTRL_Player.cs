using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using Photon.Pun.UtilityScripts;

public class CTRL_Player : MonoBehaviourPunCallbacks
{
    public static CTRL_Player instance;
    public float speed = 10f;
    public int stackSlow = 0;

    [SerializeField] public Joystick joystick;
    [SerializeField] public GameObject joyButton;

    [SerializeField] GameObject scoreBoard;
    int playerCount;


    private Rigidbody rb;
    private float GameobjectRotation2;

    public bool isShooting;
    Vector2 GameobjectRotation;

    CTRL_Animation anim;
    PhotonView PV;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        anim = GetComponent<CTRL_Animation>();
        joystick = FindObjectOfType<Joystick>();

        scoreBoard.SetActive(false);
    }
    void Start()
    {
        if (instance != null)
        {
            instance = null;
        }
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(GetComponentInChildren<CinemachineVirtualCamera>().gameObject);
            GetComponentInChildren<Joystick>().gameObject.SetActive(false);
            joyButton.SetActive(false);
        }
    }

    void Update()
    {
        SlowDown();
        if (PV.IsMine)
        {
            Move();
            ScoreBoard();
            //HigthScore();

            if (joystick.Horizontal != 0 || joystick.Vertical != 0 || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                anim.Walk();

            }
            else if (joystick.Horizontal == 0 && joystick.Vertical == 0 || Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {

                if (isShooting == false)
                {
                    anim.Idle();
                }
            }

        }
    }

    public void Move()
    {
        Vector3 temp = new Vector3(joystick.Horizontal * speed + Input.GetAxis("Horizontal") * speed
                                    , rb.velocity.y - 1
                                    , joystick.Vertical * speed + Input.GetAxis("Vertical") * speed);

        //movement
        rb.velocity = temp;


        //rotation
        GameobjectRotation = new Vector2(joystick.Horizontal, joystick.Vertical);
        if (GameobjectRotation.y > 0)
        {
            //Rotates the object if the player is facing right
            GameobjectRotation2 = GameobjectRotation.x * 90 + GameobjectRotation.y;
            this.transform.rotation = Quaternion.Euler(0f, GameobjectRotation2, 0f);
        }
        else if (GameobjectRotation.y < 0)
        {
            //Rotates the object if the player is facing left
            GameobjectRotation2 = GameobjectRotation.x * 90 + GameobjectRotation.y;
            this.transform.rotation = Quaternion.Euler(0f, -GameobjectRotation2 + 180, 0f);
        }
    }

    void ScoreBoard()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            scoreBoard.SetActive(true);
            UpdateScoreBoard();
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            scoreBoard.SetActive(false);
        }
    }
    void UpdateScoreBoard()
    {
        //check Playercount
        playerCount = PhotonNetwork.PlayerList.Length;
        //Get plyer Name
        var playerList = new StringBuilder();

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            //Debug.Log("Name: " + p.NickName + " Score: " + p.GetScore() + "\n");
            playerList.Append("Name: " + p.NickName + " Score: " + p.GetScore() + "\n");
        }

        //ScoreBoardUpdate
        string output = "All Player : " + playerCount.ToString() + "\n" + playerList.ToString();
        scoreBoard.transform.Find("Text").GetComponent<Text>().text = output;
    }
    private void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Teleport":
                transform.position = col.transform.GetChild(0).position;
                break;
        }
    }
    void SlowDown()
    {
        if (stackSlow >= 3)
        {
            speed -= 5;
            stackSlow = 0;
            StartCoroutine(ReturnSpeed());
        }
    }
    IEnumerator ReturnSpeed()
    {
        yield return new WaitForSeconds(2.5f);
        speed += 5;
    }
}
