using UnityEngine;
using System.Collections;

public class AImob : MonoBehaviour {

	public float speed;
	private float attackTimer;
	public float timer;
	public Transform destroyedExplosion;
	public Transform hitEnemie;
	public bool toDestroy;

	public LevelProperties level;
	public bool canAtack;
	
	internal bool moving = false;

	private int health;	
	
	void Start () 
	{
		speed = 5f;
		health = 3;
		attackTimer = 1.5f;
		timer = 0.0f;

		level = GameObject.Find ("Level").GetComponent<LevelProperties> ();

		canAtack = true;
		toDestroy = false;
	}
	
	void Update () 
	{
		if(!canAtack)
		{
			canAtack = false;
			timer += Time.deltaTime;
			if(timer >= attackTimer)
			{
				canAtack = true;
				timer = 0.0f;
			}
		}

		if(canAtack)
		{
			Ray ray = new Ray();
		
			if (this.gameObject.tag == "Clown")
			{
				ray = new Ray(transform.position + transform.forward * 0.5f, transform.forward);
			}
			else if (this.gameObject.tag == "Spider")
			{
				ray = new Ray(transform.position + transform.forward * 0.75f, transform.forward);
			}
			else if (this.gameObject.tag == "Dog")
			{
				ray = new Ray(transform.position + transform.forward * 2f, transform.forward);
			}
			
			RaycastHit hit = new RaycastHit();
			Debug.DrawRay(ray.origin, ray.direction, Color.red);
			if (Physics.Raycast(ray, out hit, 1f))
			{
				if(hit.collider.GetType() == typeof(CapsuleCollider) && hit.collider.gameObject.name == "Character")
				{
					hit.collider.gameObject.GetComponent<Pushback>().PushEnemy();
					hit.collider.gameObject.GetComponent<PlayerGetDamaged>().PlayerDamaged();
					hit.collider.gameObject.GetComponent<Combos>().comboCounter = 0;

					this.GetComponent<MobAttack>().EnemyAttackAnim();

					canAtack = false;
				}
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		float step = speed * Time.deltaTime;
		if(other.name == "Character")
		{
			if(!GetComponent<Pushback>().pushBacking)
			{
				moving = true;
				
				float x = other.transform.position.x;
				float y = other.transform.position.y;
				float z = other.transform.position.z;

				float rotationSpeed = 100f;

				Vector3 direction = new Vector3(x,y,z);
				Quaternion rotation = Quaternion.LookRotation(direction - transform.position);

				if((direction - transform.position).magnitude >= 0.5f)
				{
					transform.position = Vector3.MoveTowards(transform.position, direction, step);
					transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
				}

				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);


			}
			else
			{
				moving = false;
			}
		}
	}

	public void setHalth(int value)
	{
		health = value;
	}

	public void doDamage(int value)
	{
		health -= value;
		Vector3 tempPosHit = transform.position;
		tempPosHit.y = 0.1f;
		Instantiate (hitEnemie, tempPosHit, transform.rotation);
	}

	public int getHealth()
	{
		return health;
	}

	public void die()
	{
		Instantiate (destroyedExplosion, transform.position, transform.rotation);
		Spawning.m_numberOfMobs--;
		level.updatEnnemy(this.gameObject.tag);
		CharacterProperties.increaseFear(1);	//Increase by 1 on kill
		
		Destroy(this);
	}
}
