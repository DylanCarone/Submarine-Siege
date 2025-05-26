using System;
using UnityEngine;

public class Invisible : EnemyBase
{
    [SerializeField] private float stopInterval = 3f; // Time between stops
    [SerializeField] private float stopDuration = 1f; // How long to stay stopped
    [SerializeField] private Transform firePoint; // Where bullets will come from
    [SerializeField] private Bullet bulletPrefab; // The bullet prefab
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;
    
    private float originalSpeed;
    private float stopTimer;
    private float resumeTimer;
    private bool isStopped = false;
    private bool hasInitialized = false;

    public override void Initialize(int lane, bool spawnFromLeft)
    {
        base.Initialize(lane, spawnFromLeft);
        originalSpeed = MovementSpeed;
        stopTimer = stopInterval; // Start timer at full to allow initial movement
        hasInitialized = true;
        
        // If no fire point is set, use this object's position
        if (firePoint == null)
        {
            firePoint = transform;
        }
    }

    protected override void FixedUpdate()
    {
        if (!hasInitialized)
            return;
        
        if (isStopped)
        {
            // Handle resume timer
            resumeTimer -= Time.fixedDeltaTime;
            if (resumeTimer <= 0)
            {
                ResumeMovement();
            }
        }
        else
        {
            // Handle stop timer
            stopTimer -= Time.fixedDeltaTime;
            if (stopTimer <= 0)
            {
                StopAndShoot();
            }
        }
        
        // Always call base for movement
        base.FixedUpdate();
    }
    
    void StopAndShoot()
    {
        // Store original speed and stop movement
        isStopped = true;
        MovementSpeed = 0;
        
        // Set resume timer
        resumeTimer = stopDuration;
        
        // Fire a bullet
        ShootAtPlayer();
    }
    
    void ResumeMovement()
    {
        // Restore original movement speed
        MovementSpeed = originalSpeed;
        isStopped = false;
        
        // Reset stop timer
        stopTimer = stopInterval;
    }
    
    void ShootAtPlayer()
    {
        // Find player position (you might want to cache this reference)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null && bulletPrefab != null)
        {
            // Calculate direction to player
            Vector2 direction = (player.transform.position - firePoint.position).normalized;
            
            // Instantiate bullet
            Bullet bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            
            // Initialize bullet with speed and direction
            bullet.Initialize(2, 1);
        }
    }

    public override int PickLane(int maxLanes)
    {
        return Mathf.Min(2, maxLanes - 1); // Pick lane 2 if available, otherwise highest lane
    }
}