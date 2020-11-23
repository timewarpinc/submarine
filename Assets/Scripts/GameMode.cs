using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GameMode : MonoBehaviour
{
    [Serializable] public class GameObjectEvent : UnityEvent<GameObject> { }

    [SerializeField] public GameObjectEvent winEvent;
    [SerializeField] public GameObjectEvent loseEvent;
    [SerializeField] public GameObjectEvent takeDamageEvent;

    private void Start()
    {
        if (takeDamageEvent == null)
            takeDamageEvent = new GameObjectEvent();

        takeDamageEvent.AddListener((gameObject) => { loseEvent.Invoke(gameObject); });
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            OnTriggerEnter(contact.otherCollider);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherColliderGameObject = other.gameObject;

        switch (otherColliderGameObject.tag)
        {
            case "Finish":
                winEvent.Invoke(otherColliderGameObject);
                break;
            case "Damage":
                takeDamageEvent.Invoke(otherColliderGameObject);
                break;
        }
    }
}
