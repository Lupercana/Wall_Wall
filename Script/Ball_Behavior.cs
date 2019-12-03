﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Behavior : MonoBehaviour
{
    public GameObject instantiation_object;
    public float speed = 10f;
    public float speed_scale = 1.25f;
    public float immunity_seconds = 1f;

    private Rigidbody self_rbody;
    private Renderer self_renderer;
    private ParticleSystem self_particles;
    private ParticleSystem.MainModule self_particle_settings;
    private Color current_color;
    private float init_time;

    public Color GetColor()
    {
        Color t_color = current_color;

        SetColor(Color.gray);

        return t_color;
    }

    private void SetColor(Color to_set)
    {
        current_color = to_set;
        self_renderer.material.SetColor("_Color", current_color);
    }

    private void Awake()
    {
        self_rbody = GetComponent<Rigidbody>();
        self_renderer = GetComponent<Renderer>();
        self_particles = GetComponent<ParticleSystem>();
        self_particle_settings = self_particles.main;
    }

    private void Start()
    {
        SetColor(Color.gray);
        Vector3 random_dir = new Vector3(Random.value * 360, Random.value * 360, Random.value * 360);
        transform.Rotate(random_dir);
        self_rbody.AddForce(transform.forward * speed);
        init_time = Time.time;
    }

    private void Update()
    {
        self_rbody.velocity = self_rbody.velocity.normalized * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool triggered = false;
        Color new_color = Color.gray;
        if (collision.transform.tag == "Wall")
        {
            new_color = collision.transform.GetComponent<Wall_Behavior>().GetColor();
            triggered = true;
            
        }
        else if (collision.transform.tag == "Ball" && (Time.time - init_time) > immunity_seconds)
        {
            new_color = collision.transform.GetComponent<Ball_Behavior>().GetColor();
            triggered = true;
        }

        if (triggered && new_color != Color.gray)
        {
            SetColor(new_color);

            // Do different things based on current_color of the wall hit
            if (new_color == Color.yellow)
            {
                GameObject inst = Instantiate(instantiation_object, transform.position, Quaternion.identity);
                inst.GetComponent<Ball_Behavior>().speed = speed;
            }
            else if (new_color == Color.blue)
            {
                speed /= speed_scale;
            }
            else if (new_color == Color.green)
            {
                speed *= speed_scale;
            }

            self_particle_settings.startColor = new ParticleSystem.MinMaxGradient(new_color);
            self_particles.Play();
        }
    }
}
