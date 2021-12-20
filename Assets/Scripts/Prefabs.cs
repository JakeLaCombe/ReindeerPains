using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    // Start is called before the first frame update
    public static Prefabs instance;
    public Player player;
    public MockingBird MOCKING_BIRD;
    public Smoke SMOKE;
    public SmokeTrap SMOKE_TRAP;
    public VaccineProjectile VACCINE_PROJECTILE;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
