using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarMover : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float SpeedGainPerSecond = 0.3f;
    [SerializeField] private float turnSpeed = 200f;

    private int steerValue;
    
    void Update()
    {
        speed += SpeedGainPerSecond * Time.deltaTime;
        transform.Rotate(0, steerValue * turnSpeed * Time.deltaTime, 0);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) SceneManager.LoadScene(0); //("SceneMainMenu");
    }

    public void Steer(int value)
    {
        steerValue = value;
    }
}
