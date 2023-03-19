using UnityEngine;

public class Character_Controller : MonoBehaviour, IDataPersistance
{
    [SerializeField] float _speed = 5f;
    [SerializeField] CharacterController _controller;
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;
    [SerializeField] HealthBar healthBar;
    [SerializeField] int money = 0;
    [SerializeField] MoneyUI moneyUI;
    public Camera cam;

    float turnSmoothVelocity;
    public static bool teleported = false;
    public static Vector3 playerVec { get; private set; }
    private float gravity = 8f;
    private float vSpeed = 0f;
    public Money moneySO;

    Animator _animator;
    void Awake()
    {

        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        //Temporary for bandit steal attack
        money = moneySO.money;
        moneyUI.SetMoney(money.ToString());
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            Move();
        }
        if (Input.GetKeyDown("space"))
        {
            TakeDamage(10);
        }
        if (teleported == true)
        {
            transform.position = playerVec;
            teleported = false;
        }

    }

    private void OnEnable()
    {
        SoundManager.SetGenereEvent(SoundGenere.BACKGROUND);
    }

    public void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        if (movement.sqrMagnitude > 1)
        {
            movement = movement.normalized;
        }
        if (_controller.isGrounded)
        {
            vSpeed = 0;
        }
        movement.y = vSpeed;
        if (movement.magnitude > 0)
        {
            float targetAngle = Mathf.Atan2(-movement.x, -movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            vSpeed -= gravity * Time.deltaTime;
            _controller.Move(movement * _speed * Time.deltaTime);
        }
        float velocity = movement.magnitude;
        _animator.SetFloat("Velocity", velocity, 0.1f, Time.deltaTime);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void AddMoney(int amount)
    {
        money += amount;
        moneySO.money += amount;
        moneyUI.SetMoney(money.ToString());
        LootPopUpManager.instance.PopUpLoot("Coins", amount);
    }
    public int GetMoney()
    {
        return money;
    }

    public static void teleportPlayer(Vector3 vec)
    {
        teleported = true;
        playerVec = vec;
    }

    public void LoadData(GameData data)
    {
        Debug.Log($"loading data:\nmovemet: {data.playerPos},\nmoney: {data.money}");
        this.transform.position = data.playerPos;
        this.money = data.money;
    }

    public void SaveData(ref GameData data)
    {
        
        data.money = this.money;
        data.playerPos = this.transform.position;
        Debug.Log($"saving data: {money}, pos: {data.playerPos}");
    }
}
