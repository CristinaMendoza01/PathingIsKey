using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public int playerIndex;

    public Vector3 tmppos;
    public Vector3 tmppos_offset;

    // Variable to calculate the direction
    private Vector3 lastPosition;
    public Vector3 direction;

    private AudioSource pickGemAudio;
    public AudioClip pickGemSound;

    // Start is called before the first frame update
    void Start()
    {
        tmppos = new Vector3(79.5f, 0, 76.8f);
        tmppos_offset = new Vector3(0f, 0, 0f);
        lastPosition = transform.position;
        pickGemAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate the direction
        direction = (transform.position - lastPosition).normalized;
        lastPosition = transform.position;
    }

    public void setPosition(Vector3 pos)
    {
        float x = Mathf.Clamp(pos.x, tmppos.x - tmppos_offset.x, tmppos.x + tmppos_offset.x);
        float y = Mathf.Clamp(pos.y, 0, 8);
        float z = Mathf.Clamp(pos.z, tmppos.z - tmppos_offset.z, tmppos.z + tmppos_offset.z);

        transform.position = new Vector3(x, y, z);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Platform") || col.CompareTag("Stone") || col.CompareTag("WaterStone"))
        {
            //isValidpos = true;
            tmppos = col.transform.position;
            tmppos_offset = col.transform.localScale * 10 / 2;
        }

        if(col.CompareTag("Gem"))
        {
            col.gameObject.SetActive(false);
            pickGemAudio.PlayOneShot(pickGemSound, 1.0f); 
        }
    }

}
