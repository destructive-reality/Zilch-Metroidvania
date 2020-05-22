using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] protected int currentHealth;
    [SerializeField] protected Color firstColor;
    [SerializeField] protected Animator animator;


    // Start is called before the first frame update
    protected void Start()
    {
        currentHealth = maxHealth;
        firstColor = GetComponent<MeshRenderer>().material.color;
    }

    public void getHit(int damage)
    {
        Debug.Log(gameObject.name + " gets Hit for " + damage);
        currentHealth -= damage;
        // Animation goes here
        // animator.SetTrigger("Hurt");
        flashRed();
        if (currentHealth <= 0)
        {
            die();
        }
    }
    protected void flashRed()     // reaktion to taking damage, might change later    MD
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        Invoke("setbackColor", 0.3f);
        // yield return new WaitForSeconds(0.3f);   // didnt work in IEnuerator
        // GetComponent<MeshRenderer>().material.color = firstColor;
    }
    protected void setbackColor()
    {
        GetComponent<MeshRenderer>().material.color = firstColor;
    }
    protected void die()
    {
        Debug.Log(gameObject.name + " is dying. Bye!");
        // Animation goes here; Maybe better to call Animation via Main-Script    MD
        // animator.SetBool("IsDead", true);
        foreach (Behaviour childComponent in gameObject.GetComponentsInChildren<Behaviour>())
        {
            childComponent.enabled = false;
        }
        // GetComponent<Collider2D>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        // this.enabled = false;
    }
}
