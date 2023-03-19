using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollision : MonoBehaviour
{
    private PolygonCollider2D playerCollider;
    public BoxCollider2D enemyCollider;
    public GameObject enemy;
    private XmlSerializer vectorSerializer = new XmlSerializer(typeof(Vector3));

    private void Start()
    {
        if (File.Exists("enemyDead.xml"))
        {
            File.Delete("enemyDead.xml");
            Destroy(enemy);
            StreamReader reader = new StreamReader("position.xml");
            Vector3 readVector = (Vector3)vectorSerializer.Deserialize(reader);
            transform.position = readVector;
            reader.Close();
            File.Delete("position.xml");


        }
        playerCollider = GetComponent<PolygonCollider2D>();
    }
    void Update()
    {
        if (enemyCollider != null)
        {
            if (playerCollider.IsTouching(enemyCollider))
            {
                StreamWriter writer = new StreamWriter("position.xml");
                vectorSerializer.Serialize(writer, transform.position);
                SceneManager.LoadScene("BattleScene");
                writer.Close();
            }
        }

    }
}
