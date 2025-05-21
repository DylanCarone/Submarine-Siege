using System;
using UnityEngine;

public class TopDownShooterPrototype : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject playerBullet;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float bulletSpeed; 
    private Rigidbody2D playerRB;

    private Vector2 playerMovement;

    private void Start()
    {
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        playerMovement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 direction = (mousePos - player.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetMouseButtonDown(0))
        {
            var newBullet = Instantiate(playerBullet, player.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
            Destroy(newBullet, 3f);
        }

    }


    private void FixedUpdate()
    {
        Vector2 moveDelta = new Vector2(playerMovement.x, playerMovement.y) * (playerSpeed * Time.fixedDeltaTime);
        Vector2 newPosition = playerRB.position + moveDelta;
        playerRB.MovePosition(newPosition);
    }
}
