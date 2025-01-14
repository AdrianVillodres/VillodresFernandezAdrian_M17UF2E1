using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonRotation : MonoBehaviour
{
    public Vector3 _maxRotation = new Vector3(0, 0, 90);
    public Vector3 _minRotation = new Vector3(0, 0, 15);
    private float offset = -51.6f;
    public GameObject ShootPoint;
    public GameObject Bullet;
    public float ProjectileSpeed = 0;
    public float MaxSpeed;
    public float MinSpeed;
    public GameObject PotencyBar;
    private float initialScaleX;

    private void Awake()
    {
        initialScaleX = PotencyBar.transform.localScale.x;
    }
    void Update()
    {
        //PISTA: mireu TOTES les variables i feu-les servir

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = transform.position - mousePos;
        var angle = (Mathf.Atan2(direction.y, direction.x) * 180f / Mathf.PI - offset);
        Debug.Log(angle);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Input.GetMouseButton(0))
        {
            if(ProjectileSpeed < MaxSpeed)
            {
                ProjectileSpeed += Time.deltaTime * 4;
            }
            
        }
        if(Input.GetMouseButtonUp(0))
        {
            var projectile = Instantiate(Bullet, ShootPoint.transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = ProjectileSpeed * -transform.position.normalized;
            ProjectileSpeed = 0f; //reset despr�s del tret
        }
        CalculateBarScale();

    }
    public void CalculateBarScale()
    {
        PotencyBar.transform.localScale = new Vector3(Mathf.Lerp(0, initialScaleX, ProjectileSpeed / MaxSpeed),
            PotencyBar.transform.localScale.y,
            PotencyBar.transform.localScale.z);
    }
}
