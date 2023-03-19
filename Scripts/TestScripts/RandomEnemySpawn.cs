using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomEnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float timer;
    private float starttimer;
    GameObject Hero;
    private bool turnOn = true;
    private bool spawnEns = true;
    void Start()
    {
        Hero = GameObject.FindGameObjectWithTag("Player"); ;
        timer = 3;
        starttimer = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (starttimer > 0)
            starttimer -= Time.deltaTime;
        if (starttimer <= 0 && spawnEns == true)
        {
            Quaternion rotation1 = Quaternion.Euler(0, 30, 0);

            Instantiate(enemyPrefab, new Vector3(-256, 2, -36), Quaternion.Euler(0, -60, 0));
            Instantiate(enemyPrefab, new Vector3(-258, 2, -35), Quaternion.Euler(0, 180, 0));
            Instantiate(enemyPrefab, new Vector3(-260, 2, -36), Quaternion.Euler(0, 115, 0));
            spawnEns = false;
        }
        if (SceneManager.GetActiveScene().name != "3D Hero test")
            turnOn = false;
        if (timer <= 0 && turnOn == true)
        {
            //   Instantiate(enemyPrefab, new Vector3(Hero.transform.position.x + 10, Hero.transform.position.y, Hero.transform.position.z), transform.rotation);
            timer = 5;
        }

    }
}
