using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;
using System;

public class MonsterSpawnerController : Controller<MonsterSpawnerView> {

    private Cooldown monsterSpawnCooldown;
    private Cooldown waveSpawnCooldown;
    private WavesData wavesData;

    private WaveService waveService;
    private int monstersSpawned = 0;

    private List<MonsterController> controllers = new List<MonsterController>();

    private GoldManagerController goldManagerController;

	public MonsterSpawnerController(MonsterSpawnerView view, GoldManagerController goldManagerController, WaveService waveService) : base(view)
    {
        this.goldManagerController = goldManagerController;

        wavesData = new LevelOneWavesData();
        monsterSpawnCooldown = new Cooldown(WavesData.MONSTER_SPAWN_INTERVAL);
        waveSpawnCooldown = new Cooldown(WavesData.WAVE_SPAWN_INTERVAL);
        this.waveService = waveService;
	}

    public void Update(float deltaTime)
    {
        monsterSpawnCooldown.Update(deltaTime);
        waveSpawnCooldown.Update(deltaTime);

        waveService.CollectKillRewards();
        goldManagerController.UpdateView();

        SpawnNextWave();
        SpawnNextMonster();
	}

    public void KillWave()
    {
        waveService.CurrentWave.Kill();

        foreach (MonsterController controller in controllers)
            controller.UpdateView();

        PrepareForNextWave();
    }

    private void SpawnNextWave()
    {
        if (waveService.CurrentWave == null || waveService.CurrentWave.IsDead)
        {
            if (waveService.WavesSpawned < wavesData.Data.Length)
            {
                waveService.Spawn(wavesData.Data[waveService.WavesSpawned]);
                PrepareForNextWave();
            }
        }
    }

    private void PrepareForNextWave()
    {
        monstersSpawned = 0;
        waveSpawnCooldown.Reset();
        controllers.Clear();
    }

    private void SpawnNextMonster()
    {
        if (monstersSpawned < waveService.CurrentWave.Monsters.Count && monsterSpawnCooldown.IsOver && waveSpawnCooldown.IsOver)
        {
            WaveData currentWaveData = wavesData.Data[waveService.WavesSpawned - 1];
            MonsterType monsterType = currentWaveData.MonsterTypes[monstersSpawned];
            MonsterController controller = MonsterFactory.CreateMonster(monsterType, waveService.CurrentWave.Monsters[monstersSpawned]);
            controller.UpdateView();
            controllers.Add(controller);

            monsterSpawnCooldown.Reset();
            monstersSpawned++;
        }
    }

    public override void UpdateView()
    {

    }
}
