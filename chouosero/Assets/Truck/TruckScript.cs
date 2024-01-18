using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckScript : MonoBehaviour
{
    public AudioClip hitSE;
    AudioSource audioSource;
    public int hitcall = 0;
    public int HP = 10;
    public bool broke = false;
    public float impact = 1200.0f;
    public float speed = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(hitcall>=HP&&!broke)
        {
            broke = true;
            var dir = transform.rotation * Vector3.right;
            dir *= -speed;
            GetComponent<Rigidbody>().AddForce(dir*(impact/2));
        }
        if(!broke)
        {
            var dir = transform.rotation * Vector3.right;
            dir *= speed;
            transform.position += dir* Time.deltaTime;
            
        }

    }
    private void OnTriggerEnter(Collider collision)
    {

        // è’ìÀÇµÇΩÇÁÅc
        if (collision.CompareTag("audience"))
        {
            collision.GetComponent<RagDollCtrl>().RagDoll = true;
            impactHuman(collision);
            audioSource.PlayOneShot(hitSE);
        }
        else if(collision.CompareTag("RagDoll"))
        {
            impactHuman(collision);
        }
        else if(collision.CompareTag("koma"))
        {
            collision.GetComponentInParent<Animator>().enabled = false;
            impactObject(collision);

        }
        else if (!collision.CompareTag("Ground") && !collision.CompareTag("Truck") )//&& !collision.CompareTag("RagDoll"))
        {
            Rigidbody rb = collision.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            Vector3 vector;
            vector = collision.transform.position - this.transform.position;
            vector.y += 1.0f;
            vector.Normalize();
            rb.AddForce(vector * impact);
            hitcall++;
            audioSource.PlayOneShot(hitSE);
        }
    }
    void impactHuman(Collider collision)
    {
        Vector3 vector;
        vector = collision.transform.position - this.transform.position;
        vector.y += 1.0f;
        vector.Normalize();
        collision.GetComponent<Rigidbody>().AddForce(vector * impact * 2);
        collision.GetComponent<Rigidbody>().AddForce(Vector3.up * 50);
        hitcall++;
    }
    void impactObject(Collider collision)
    {
        Rigidbody rb = collision.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        Vector3 vector;
        vector = collision.transform.position - this.transform.position;
        vector.y += 1.0f;
        vector.Normalize();
        rb.AddForce(vector * impact);
    }
}
