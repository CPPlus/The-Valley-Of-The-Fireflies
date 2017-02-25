using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;

public class SpawnerController {

    public SpawnerView View { get; set; }

    private Cooldown monsterSpawnCooldown;
    private Cooldown waveSpawnCooldown;
    private WavesData wavesData;

    private WaveService waveService;
    private int monstersSpawned = 0;

    private List<MonsterController> controllers = new List<MonsterController>();

	public SpawnerController()
    {
        wavesData = new LevelOneWavesData();
        monsterSpawnCooldown = new Cooldown(WavesData.MONSTER_SPAWN_INTERVAL);
        waveSpawnCooldown = new Cooldown(WavesData.WAVE_SPAWN_INTERVAL);
        waveService = new WaveService();
	}
	
	public void Update(float deltaTime)
    {
        monsterSpawnCooldown.Update(deltaTime);
        waveSpawnCooldown.Update(deltaTime);

        if (waveService.CurrentWave == null || waveService.CurrentWave.IsDead)
            if (wavesData.Data.Length - 1 >= waveService.WavesSpawned)
                waveService.Spawn(wavesData.Data[waveService.WavesSpawned]);

        SpawnWaves();
	}

    public void KillWave()
    {
        waveService.CurrentWave.Kill();
        monstersSpawned = 0;
        waveSpawnCooldown.Reset();

        foreach (MonsterController controller in controllers)
            controller.UpdateView();

        controllers.Clear();
    }

    private void SpawnWaves()
    {
        if (monstersSpawned < waveService.CurrentWave.Monsters.Count && monsterSpawnCooldown.IsOver && waveSpawnCooldown.IsOver)
        {
            MonsterController controller = new MonsterController();
            controller.Model = waveService.CurrentWave.Monsters[monstersSpawned];
            View.SpawnMonsterView(controller, wavesData.Data[waveService.WavesSpawned - 1].MonsterTypes[monstersSpawned]);
            controller.UpdateView();
            controllers.Add(controller);

            monsterSpawnCooldown.Reset();
            monstersSpawned++;
        }
    }
}
