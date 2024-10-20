using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Collections;
public class PopulationManager_sc : MonoBehaviour
{
    public GameObject personPrefab; //select person
    public int populationSize=10; //size of population
    private List<GameObject> population = new List<GameObject>(); //list of people
    public static float elapsed=0; //time elapsed her cycle
    int trialTime = 10,generation = 1; /*10 saniye içinde tamamını avlayacağımızı (fare sol tık) varsayıyoruz. generation
    Hangi jenerasyonda olduğumuzu saklar*/
    GUIStyle guiStyle=new GUIStyle();
    void OnGUI()
    {           // Ekranda jenerasyon no ve geçen sürenin gösterimi için
        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10,10,100,20), "Generation: " + generation, guiStyle);
        GUI.Label(new Rect(10,30,100,20), "Time: " + (int) elapsed, guiStyle);
    }
    // Start is called before the first frame update
   public void Start()
    {
        for (int i=0; i<populationSize; i++){ //creat a new population
            Vector3 pos=new Vector3(Random.Range(-9.5f, 9.5f), Random.Range(-3.4f, 3.4f),0);
            GameObject o_newPerson=Instantiate(personPrefab, pos, Quaternion.identity);
            o_newPerson.GetComponent<DNA_sc>().r=Random.Range(0.0f,1.0f);
            o_newPerson.GetComponent<DNA_sc>().g=Random.Range(0.0f,1.0f);
            o_newPerson.GetComponent<DNA_sc>().b=Random.Range(0.0f,1.0f);
            o_newPerson.GetComponent<DNA_sc>().s = Random.Range(0.1f, 0.3f);
            population.Add(o_newPerson);
        }
    }

   public void BreedNewPopulation()
   {
        List<GameObject> newPopulation = new List<GameObject>();
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA_sc>().TimetoDie).ToList();
        population.Clear();
        for (int i = (int) (sortedList.Count/2.0f)-1; i<sortedList.Count-1; i++){
            population.Add(Breed(sortedList[i], sortedList[i+1]));
            population.Add(Breed(sortedList[i+1], sortedList[i]));
        }
        for (int i = 0; i < sortedList.Count; i++){
            Destroy(sortedList[i]);
        }
            generation++;
    }
    GameObject Breed(GameObject parent1, GameObject parent2){
        Vector3 pos = new Vector3(Random.Range(-9.5f,9.5f), Random.Range(-3.4f,5.4f), 0);
        GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);
        DNA_sc dna1 = parent1.GetComponent<DNA_sc>();
        DNA_sc dna2 = parent2.GetComponent<DNA_sc>();
        offspring.GetComponent<DNA_sc>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
        offspring.GetComponent<DNA_sc>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
        offspring.GetComponent<DNA_sc>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;
               
        if (Random.Range(0,10) < 5) {
            offspring.GetComponent<DNA_sc>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
            offspring.GetComponent<DNA_sc>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
            offspring.GetComponent<DNA_sc>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;
            offspring.GetComponent<DNA_sc>().s = Random.Range(0, 10) < 5 ? dna1.s : dna2.s;
            } else { //%50 mutasyon ihtimali. Genelde mutasyon ihtimali çok daha düşüktür
            offspring.GetComponent<DNA_sc>().r = Random.Range(0.0f, 1.0f);
            offspring.GetComponent<DNA_sc>().g = Random.Range(0.0f, 1.0f);
            offspring.GetComponent<DNA_sc>().b = Random.Range(0.0f, 1.0f);
            offspring.GetComponent<DNA_sc>().s = Random.Range(0.1f, 0.3f);
            }
        return offspring;
    }
    // Update is called once per frame
    void Update()
    { //Her jenerasyon 10 saniye sürecek ve ardından yeni jenerasyon oluşturulacak
        elapsed += Time.deltaTime;
        if (elapsed > trialTime)
            {
                BreedNewPopulation();
                elapsed = 0;
            }
    }
}
