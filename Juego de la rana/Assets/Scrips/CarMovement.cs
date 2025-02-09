using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private float speed; // Velocidad del coche.

    void Start()
    {
        // Inicializamos la velocidad usando Random.Range en Start (o en Awake, dependiendo de tu lógica).
        speed = Random.Range(8f, 1f); // Asegúrate de que Random.Range se llame aquí.
    }

    void Update()
    {
        // Asegúrate de que el coche se mueve en la dirección correcta.
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Destruye el coche si está fuera del rango.
        if (transform.position.x > 150f || transform.position.x < -150f)
        {
            Destroy(gameObject);
        }
    }
}
