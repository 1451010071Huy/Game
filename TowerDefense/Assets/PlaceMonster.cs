using UnityEngine;
using System.Collections;

public class PlaceMonster : MonoBehaviour {

	public GameObject monsterPrefab;
	private GameObject monster;
	private GameManagerBehavior gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private bool canPlaceMonster() {
        
        monsterPrefab = gameManager.ClickedBtn.TowerPrefab;
		int cost = monsterPrefab.GetComponent<MonsterData> ().levels[0].cost;
		return monster == null && gameManager.Gold >= cost;
	}
	public void PlaceMonsters()
    {

        monster = (GameObject)Instantiate(gameManager.ClickedBtn.TowerPrefab, transform.position, Quaternion.identity);
        //4
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);
       
        gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
 
    }
	//1
    void OnMouseOver()
    {
        //2
        if (Input.GetMouseButtonDown(0))
        {
            if (canPlaceMonster())
            {
                //3
                PlaceMonsters();
             //   gameManager.Unclick();
            }
            else if (canUpgradeMonster())
            {
                monster.GetComponent<MonsterData>().increaseLevel();
                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                audioSource.PlayOneShot(audioSource.clip);

                gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
            }
        }
    }

	private bool canUpgradeMonster() {
		if (monster != null) {
			MonsterData monsterData = monster.GetComponent<MonsterData> ();
			MonsterLevel nextLevel = monsterData.getNextLevel();
			if (nextLevel != null) {
				return gameManager.Gold >= nextLevel.cost;
 			}
  		}
		return false;
	}
}
