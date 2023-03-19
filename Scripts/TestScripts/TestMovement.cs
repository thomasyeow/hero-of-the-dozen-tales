using UnityEngine;

public class TestMovement : MonoBehaviour
{

    //SIMPLE MOVEMENT CLASS TO TEST OTHER SCRIPTS

    public int xSpeed = 5;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        float xDisplacement = Input.GetAxis("Horizontal");
        float yDisplacement = Input.GetAxis("Vertical");
        Vector3 displacementVector = new Vector3(xDisplacement, 0, yDisplacement);


        transform.Translate(displacementVector * Time.deltaTime * xSpeed);
    }


}

