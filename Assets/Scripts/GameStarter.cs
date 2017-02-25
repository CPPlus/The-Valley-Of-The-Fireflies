using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour {

    SpawnerController spawnerController;

    void Start () {
        spawnerController = new SpawnerController();
        spawnerController.View = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnerView>();
	}
	
	void Update () {
        spawnerController.Update(Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.K))
            spawnerController.KillWave();
	}
}
