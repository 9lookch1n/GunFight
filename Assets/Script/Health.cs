using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Pun.UtilityScripts;

public class Health : MonoBehaviourPunCallbacks, IPunObservable
{
    public static Health instance;

    public float maxHealth = 100f;
    public float currentHealth;
    public Text TextHP;
    public Image healthBar;

    PlayerManager playerManager;
    

    void Awake()
    {
        playerManager = PhotonView.Find((int)photonView.InstantiationData[0]).GetComponent<PlayerManager>();
    }
    void Start()
    {
        instance = this;

        currentHealth = maxHealth;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        TextHP.text = currentHealth.ToString();

    }
    void Update()
    {
        TextHP.text = currentHealth.ToString();
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    //HP systeme
    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //syn hp
        if (stream.IsWriting)
        {
            stream.SendNext(currentHealth);
        }
        else
        {
            currentHealth = (float)stream.ReceiveNext();
        }
    }
    public void TakeDamage(float damage)
    {
         photonView.RPC("RPC_Takedamage", RpcTarget.All, damage);
    }

   // [PunRPC]
    public void Takedamage(float damage)
    {

        if (photonView.IsMine)
        {
            Debug.Log("Take damage:" + damage);

            currentHealth -= damage;

            healthBar.fillAmount = currentHealth / maxHealth;

            TextHP.text = currentHealth.ToString();
            if (currentHealth <= 0)
            {
                //die
                Debug.Log("Die");
                //Die();
                photonView.RPC("Die", RpcTarget.All);
            }
        }
    }
    [PunRPC]
    public void Die()
    {

        /*playerManager.Die();
        PhotonNetwork.Destroy(gameObject);*/

        photonView.gameObject.SetActive(false);

        Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
        photonView.transform.position = spawnpoint.position;
        photonView.transform.rotation = spawnpoint.rotation;

        currentHealth = maxHealth;
        //checkDead();
        Invoke("checkDead", 0.5f);
        
    }
    void checkDead()
    {
        photonView.gameObject.SetActive(true);
    }

    public void addScore()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.LocalPlayer.AddScore(1);
        }
        
    }
}
