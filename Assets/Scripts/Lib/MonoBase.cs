using UnityEngine;
using System;
using System.Collections.Generic;

public class MonoBase : MonoBehaviour
{
	private List<GameObject> DestructionRegistry = new List<GameObject> ();
	private Dictionary<Type, Component> cachedComponents = new Dictionary<Type, Component> ();
	
	public T _<T> () where T :Component
	{
		if (!cachedComponents.ContainsKey (typeof(T)))
			cachedComponents [typeof(T)] = GetComponent<T> ();
		return cachedComponents [typeof(T)] as T;
	}
	
	private Transform _transform;
	public new Transform transform { get { return _transform; } }

	public new Collider collider { get { return _<Collider> (); } }

	private Rigidbody _rigidbody;
	public new Rigidbody rigidbody { get { return _rigidbody; } }
	
	public virtual void Start ()
	{
	}

	public virtual void Awake ()
	{
		_transform = _<Transform>();
		_rigidbody = _<Rigidbody>();
	}
	
	public void RegisterForDestruction (GameObject g)
	{
		DestructionRegistry.Add (g);
	}

	public void UnregisterForDestruction (GameObject g)
	{
		DestructionRegistry.Remove (g);
	}

	public virtual void OnDestroy ()
	{
		foreach (GameObject o in DestructionRegistry)
			Destroy (o);
		DestructionRegistry.Clear ();
	}
}
