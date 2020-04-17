using UnityEngine;
using System.Collections;

public class GameManage : MonoBehaviour
{
    [Header("燈光群組")]
    public GameObject GroupLight;
    [Header("移動桶子")]
    public Transform Barrel;
    [Header("移動椅子")]
    public Transform Chair;
    [Header("喇叭")]
    public AudioSource aud;
    [Header("木椅滑動音效")]
    public AudioClip SoundWoodMove;
    [Header("木桶滑動音效")]
    public AudioClip BarrelDrag;
    [Header("敲敲門音效")]
    public AudioClip KnockKnock;
    [Header("開門音效")]
    public AudioClip OpenDoor;
    [Header("門的動畫控制器")]
    public Animator aniDoor;

    public int countDoor;
    public bool OpenOK = false;

    public void LookDoor()
    {
        countDoor++;

        if (countDoor == 1 && !OpenOK)
        {
            StartCoroutine(DoorUnlock());
        }
        if(OpenOK)
        {
            OpenOK = false;
            aud.PlayOneShot(OpenDoor, 2.5f);
            aniDoor.SetTrigger("open");
        }
        if (countDoor == 1)
        {
            aud.PlayOneShot(KnockKnock, 1);
        }
    }



    public IEnumerator LightEffect()
    {
        for (int i = 1; i <= 5; i++)
        {
            GroupLight.SetActive(false);
            yield return new WaitForSeconds(Random.Range(0.05f,1f));
            GroupLight.SetActive(true);
            yield return new WaitForSeconds(Random.Range(0.05f,1f));
        }
    }

    public IEnumerator DoorUnlock()
    {
        yield return new WaitForSeconds(7);
        OpenOK = true;
    }

    public void StartMoveBarrel()
    {
        StartCoroutine(MoveBarrel());
    }

    public void StartMoveChair()
    {
        StartCoroutine(MoveChair());
    }

    public IEnumerator MoveBarrel()
    {
        Barrel.GetComponent<MeshCollider>().enabled = false;
        for (int i = 0; i <= 8; i++)
        {
            Barrel.position -= Barrel.forward * 0.4f;
            yield return new WaitForSeconds(0.01f);
        }
        aud.PlayOneShot(BarrelDrag, 2.5f);

    }
    public IEnumerator MoveChair()
    {
        Chair.GetComponent<MeshCollider>().enabled = false;
        for (int i = 0; i <= 8; i++)
        {
            Chair.position -= Chair.forward * 0.2f;
            yield return new WaitForSeconds(0.01f);
        }
        aud.PlayOneShot(SoundWoodMove, 2.5f);

    }
    public void Start()
    {
        StartCoroutine(LightEffect());
    }
}
