using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DNA_sc : MonoBehaviour
{
    //degisken
    public float r,g,b,s,TimetoDie = 0.0f; // red = 0.3 green = 0.8 blue = 0.3 s= boyut TimetoDie =yasama suresi
    bool isDead = false; // click object of dead
    SpriteRenderer sRinder; // object to dis visaply sonra olum
    Collider2D sCollider; //object arasinda not carpi
    // Start is called before the first frame update
    void Start()
    {
        sRinder=GetComponent<SpriteRenderer>();
        sCollider=GetComponent<Collider2D>();
        sRinder.color=new Color(r,g,b); // object renkleri baslangicta belirleniyor
        this.transform.localScale = new Vector3(s, s, s);
    }
    public void OnMouseDown() {
        isDead=true;
        TimetoDie = PopulationManager_sc.elapsed;
        Debug.Log("Dead At: " + TimetoDie);
        sRinder.enabled=false; // object gizleniyor
        sCollider.enabled=false; // object arasinda not carpmasi durduruluyor
    }
    // Update is called once per frame
    void Update(){    
    }
}
