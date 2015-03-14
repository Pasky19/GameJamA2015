using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Combos : MonoBehaviour {

	private string buttonPressed;
	private List<string> buttonList; 
	public List<GameObject> enemyList;

	private string combo3 = "XBB";
	private string combo4 = "XYXY";
	private string combo5 = "YXY";
	private string combo7 = "YBXX";
	private string combo8 = "ULTIMATE";
	private string attackX = "X";
	private string attackY = "Y";
	private string attackB = "B";

	private float timerEndSimpleAttack;
	private float simpleAttackTimer;
	private float timerEndCombo;
	private float comboTimer;
	private float cooldownTimer;
	private float timerEndCooldown;
	
	private int nbCol;
	private bool canCombo;
	private bool canAttack;
	private bool onCooldown;

	public int comboCounter;
	public int comboCounterIncByOne;

	public int maxComboCounterObj;
	public int nbObjectifCombo;
	
	private float comboNotHitTimer;

	MasterObjectifs Obj_Master;

	void Start () 
	{
		canCombo = false;
		timerEndCooldown = 0.0f;
		timerEndCombo = 0.0f;
		timerEndSimpleAttack = 0.0f;
		cooldownTimer = 0.5f;
		comboTimer = 0.25f;
		simpleAttackTimer = 0.2f;
		comboNotHitTimer = 2f;

		buttonList = new List<string>();
		enemyList = new List<GameObject>();

		buttonPressed = "";

		canAttack = true;
		onCooldown = false;

		comboCounter = 0;
		comboCounterIncByOne = 0;

		Obj_Master = GameObject.Find("MasterObjectif").GetComponent<MasterObjectifs>();
	}

	void Update () 
	{
		if(maxComboCounterObj < comboCounterIncByOne)
		{
			maxComboCounterObj = comboCounterIncByOne;
			Obj_Master.Level.setCombo(maxComboCounterObj);
		}



		if (comboNotHitTimer <= 0)
		{
			comboCounter = 0;
			comboCounterIncByOne = 0;
		}
		else
		{
			comboNotHitTimer -= Time.deltaTime;
		}
	
		if(nbCol > 0)
		{
			canCombo = true;
		}
		else
		{
			canCombo = false;
		}


		if(!canAttack)
		{
			timerEndSimpleAttack += Time.deltaTime;
		}

		if(timerEndSimpleAttack >= simpleAttackTimer)
		{
			canAttack = true;
		}

		if(onCooldown)
		{
			timerEndCooldown += Time.deltaTime;
			if(timerEndCooldown >= cooldownTimer)
			{
				onCooldown = false;
				timerEndCooldown = 0.0f;
			}
		}
		else
		{
			timerEndCombo += Time.deltaTime;

			if(timerEndCombo >= comboTimer)
			{
				buttonList.Clear();
				timerEndCombo = 0.0f;
			}

			if(Input.GetButtonDown("X"))
			{
				buttonPressed = "X";
				timerEndCombo = 0.0f;
				if(canAttack)
				{
					simpleAttack("X");
					canAttack = false;
					timerEndSimpleAttack = 0.0f;
				}
			}

			if(Input.GetButtonDown("Y"))
			{
				buttonPressed = "Y";
				timerEndCombo = 0.0f;
				if(canAttack)
				{
					simpleAttack("Y");
					canAttack = false;
					timerEndSimpleAttack = 0.0f;
				}
			}

			if(Input.GetButtonDown("B"))
			{
				buttonPressed = "B";
				timerEndCombo = 0.0f;
				if(canAttack)
				{
					simpleAttack("B");
					canAttack = false;
					timerEndSimpleAttack = 0.0f;
				}
			}
			
			if (Input.GetAxis("Ultimate") < -0.9f)
			{
				buttonPressed = "ULTIMATE";
				timerEndCombo = 0.0f;
				
				if (canAttack && CharacterProperties.UltimeActivate)
				{
					simpleAttack(buttonPressed);
					canAttack = false;
					timerEndSimpleAttack = 0.0f;
					CharacterProperties.resetFearProgression();
					Obj_Master.Ultime();
					comboCounter = 0;
					comboCounterIncByOne = 0;
				}
			}

			if(buttonPressed != "")
			{
				if(canCombo)
				{
					buttonList.Add(buttonPressed);
					addButton();
				}
				buttonPressed = "";
				timerEndCombo = 0.0f;
			}
		}
	}

	public void addButton()
	{
		string finalStr = "";
		
		foreach(string str in buttonList)
		{
			finalStr += str;
		}
		checkCombo(finalStr);
	}

	public void checkCombo(string str)
	{
		int damage = 0;
		

		if (str.Contains(combo3) && buttonList.Count == 3)
		{
			buttonList.Clear();
			onCooldown = true;
			transform.GetComponent<CharacterAnims>().StartCombo(1);

			damage = 2;
			doDamage(damage, GameObject.Find("ComboCollider").GetComponentInChildren<GetOverlapping>().enemyList, enemyList, false);

			if(nbCol > 0)
			{
				comboCounter += (nbCol * 2);
				comboCounterIncByOne++;
				GameObject.Find("Master").GetComponent<UIManager>().StartAppearComboObj(comboCounter);
			}
			else
			{
				comboCounter = 0;
				comboCounterIncByOne = 0;
			}
			return;
		}

		if (str.Contains(combo4) && buttonList.Count == 4)
		{
			buttonList.Clear();
			onCooldown = true;
			transform.GetComponent<CharacterAnims>().StartCombo(2);

			damage = 3;
			doDamage(damage, GameObject.Find("ComboCollider").GetComponentInChildren<GetOverlapping>().enemyList, enemyList, false);

			if(nbCol > 0)
			{
				comboCounter += (nbCol * 3);
				comboCounterIncByOne++;
				GameObject.Find("Master").GetComponent<UIManager>().StartAppearComboObj(comboCounter);
			}
			else
			{
				comboCounter = 0;
				comboCounterIncByOne = 0;
			}
			return;
		}

		if (str.Contains(combo5) && buttonList.Count == 3)
		{
			buttonList.Clear();
			onCooldown = true;
			transform.GetComponent<CharacterAnims>().StartCombo(3);

			damage = 2;
			doDamage(damage, GameObject.Find("ComboCollider").GetComponentInChildren<GetOverlapping>().enemyList, enemyList, false);

			if(nbCol > 0)
			{
				comboCounter += (nbCol * 2);
				comboCounterIncByOne++;
				GameObject.Find("Master").GetComponent<UIManager>().StartAppearComboObj(comboCounter);
			}
			else
			{
				comboCounter = 0;
				comboCounterIncByOne = 0;
			}
			return;
		}
	
		if (str.Contains(combo7) && buttonList.Count == 4)
		{
			buttonList.Clear();
			onCooldown = true;
			transform.GetComponent<CharacterAnims>().StartCombo(4);

			damage = 3;
			doDamage(damage, GameObject.Find("ComboCollider").GetComponentInChildren<GetOverlapping>().enemyList, enemyList, false);

			if(nbCol > 0)
			{
				comboCounter += (nbCol * 3);
				comboCounterIncByOne++;
				GameObject.Find("Master").GetComponent<UIManager>().StartAppearComboObj(comboCounter);
			}
			else
			{
				comboCounter = 0;
				comboCounterIncByOne = 0;
			}
			return;
		}
	}

	public void simpleAttack(string str)
	{
		int damage = 0;

		if(str == attackX)
		{
			transform.GetComponent<CharacterAnims>().StartAttack1();
			damage = 1;

			if(nbCol > 0)
			{
				comboCounter += nbCol;
				comboCounterIncByOne++;
				GameObject.Find("Master").GetComponent<UIManager>().StartAppearComboObj(comboCounter);
			}
			else
			{
				comboCounter = 0;
				comboCounterIncByOne = 0;
			}
			
			doDamage(damage, enemyList, GameObject.Find("ComboCollider").GetComponentInChildren<GetOverlapping>().enemyList, true);
		}

		else if(str == attackB)
		{
			transform.GetComponent<CharacterAnims>().StartAttack2();
			damage = 1;

			if(nbCol > 0)
			{
				comboCounter += nbCol;
				comboCounterIncByOne++;

				GameObject.Find("Master").GetComponent<UIManager>().StartAppearComboObj(comboCounter);
			}
			else
			{
				comboCounter = 0;
				comboCounterIncByOne = 0;

			}
			
			doDamage(damage, enemyList, GameObject.Find("ComboCollider").GetComponentInChildren<GetOverlapping>().enemyList, true);
		}

		else if(str == attackY)
		{
			transform.GetComponent<CharacterAnims>().StartAttack3();
			damage = 1;

			if(nbCol > 0)
			{
				comboCounter += nbCol;
				comboCounterIncByOne++;

				GameObject.Find("Master").GetComponent<UIManager>().StartAppearComboObj(comboCounter);
			}
			else
			{
				comboCounter = 0;
				comboCounterIncByOne = 0;

			}
			
			doDamage(damage, enemyList, GameObject.Find("ComboCollider").GetComponentInChildren<GetOverlapping>().enemyList, true);
		}
		
		else if (str == combo8)
		{
			buttonList.Clear();
			onCooldown = true;
			transform.GetComponent<CharacterAnims>().StartCombo(5);
			
			damage = 10;
			
			if(nbCol > 0)
			{
				comboCounter += (nbCol * 5);
				GameObject.Find("Master").GetComponent<UIManager>().StartAppearComboObj(comboCounter);
			}
			else
			{
				comboCounter = 0;
			}
			
			doDamage(damage, GameObject.Find("ComboCollider").GetComponentInChildren<GetOverlapping>().enemyList, enemyList, true);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if((col.tag == "Spider" || col.tag == "Dog" || col.tag == "Clown") && col.GetType() == typeof(CapsuleCollider))
		{
			this.enemyList.Add(col.gameObject);

			nbCol++;
			rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if((col.tag == "Spider" || col.tag == "Dog" || col.tag == "Clown") && col.GetType() == typeof(CapsuleCollider))
		{
			this.enemyList.Remove(col.gameObject);

			nbCol--;
			rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		}
	}

	private void doDamage(int damage, List<GameObject> current, List<GameObject> other, bool simpleAttack)
	{
		List<GameObject> garbage = new List<GameObject>();

		int sizeCurrent = current.Count;
		int sizeOther = other.Count;
		
		comboNotHitTimer = 2.0f;
		
		if (simpleAttack)
		{
			if (damage == 10)
			{
				GameObject.Find ("Level").GetComponent<LevelProperties>().UpdateScore(sizeCurrent * 1000);
				
				if ((sizeCurrent * 1000) > 0)
				{
					GameObject.Find("Master").GetComponent<UIManager>().StartAppearPoints(sizeCurrent * 1000);
				}
			}
			else
			{
				GameObject.Find ("Level").GetComponent<LevelProperties>().UpdateScore((sizeCurrent * 50) * comboCounter);
				
				if (((sizeCurrent * 50) * comboCounter) > 0)
				{
					GameObject.Find("Master").GetComponent<UIManager>().StartAppearPoints((sizeCurrent * 50) * comboCounter);
				}
			}
		}
		else
		{
			GameObject.Find ("Level").GetComponent<LevelProperties>().UpdateScore((damage * 500) * sizeCurrent);
			
			if (((damage * 500) * sizeCurrent) > 0)
			{
				GameObject.Find("Master").GetComponent<UIManager>().StartAppearPoints((damage * 500) * sizeCurrent);
			}
		}

		foreach(GameObject enemy in current)
		{
			try
			{
				if (enemy)
				{
					if(enemy.GetComponent<AImob>().getHealth() <= damage)
					{
						enemy.GetComponent<AImob>().toDestroy = true;
						Destroy(enemy.gameObject);
						enemy.GetComponent<AImob>().die ();
						garbage.Add(enemy);
					}
					else
					{
						enemy.GetComponent<Pushback>().PushEnemy();
						enemy.GetComponent<AImob>().doDamage(damage);
						enemy.GetComponent<AImob>().timer = 0f;
						enemy.GetComponent<AImob>().canAtack = false;
					}
				}
			}
			catch (MissingReferenceException ex)
			{
			}
		}
		
		for (int i = 0; i < sizeCurrent; i++)
		{
			try
			{
				if (current[i])
				{
					if(current[i].GetComponent<AImob>().toDestroy)
					{
						current.RemoveAt(i);
	
						sizeCurrent--;
						i--;
	
						if(i <= 0)
						{
							i = 0;
						}
	
						if(sizeCurrent < 0)
						{
							Debug.Log("dafuq");
							sizeCurrent = 0;
						}
					}
				}
			}
			catch (MissingReferenceException ex)
			{
			}

			for (int j = 0; j < sizeOther; j++)
			{
				try
				{
					if (other[j])
					{
						if(other[j].GetComponent<AImob>().toDestroy)
						{
							other.RemoveAt(j);
							
							sizeOther--;
							j--;
	
							if(j <= 0)
							{
								j = 0;
							}
	
							if(sizeOther < 0)
							{
								Debug.Log("dafuq");
								sizeOther = 0;
							}
						}
					}
				}
				catch (MissingReferenceException ex)
				{
					//GameObject.Find ("ComboCollider").GetComponent<GetOverlapping>().nbCol--;
				}
			}
			
			bool temptemp = false;
			
			do
			{
				temptemp = false;
			
				foreach(GameObject enemy in current)
				{
					if(!enemy)
					{
						current.Remove(enemy);
						temptemp = true;
						comboCounter = 0;
						comboCounterIncByOne = 0;
					}
				}
			}
			while (temptemp);
			
			temptemp = false;
			
			do
			{
				temptemp = false;
			
				foreach(GameObject enemy in other)
				{
					if(!enemy)
					{
						other.Remove(enemy);
						temptemp = true;
						comboCounter = 0;
						comboCounterIncByOne = 0;
					}
				}
			}
			while (temptemp);
			
			foreach(GameObject enemy in garbage)
			{
				if(!current.Contains(enemy) && !other.Contains(enemy))
					Destroy(enemy);
			}

			garbage.Clear();

			nbCol = enemyList.Count;
		}
	}

	public void resetButtonList()
	{
		Debug.Log("reset");
		buttonList.Clear();
	}
}
