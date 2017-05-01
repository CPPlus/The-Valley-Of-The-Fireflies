using ElementalTowerDefenseModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PrefabAttacher))]
[RequireComponent(typeof(ObjectDisabler))]
public class TowerView : ModelView<TowerController> {
    
    private PrefabAttacher attacher;
    private ObjectDisabler disabler;
    private ScaleRelativeToCamera scaleRelativeToCamera;
    private float range;

    private const int INITIAL_MILESTONE = 30;
    private int lastAmmoPercentageShown = int.MaxValue;
    private int nextMilestone = INITIAL_MILESTONE;

    private bool uiIsShown = false;

    public Bar ammoBar;
    public Button reloadButton;
    public Button sellButton;

    void Start()
    {
        FlyingTextSpawner.SpawnGoldSpent((new RegularPriceList()).GetPrice(Controller.Model.Type), gameObject);
    }

    void Update()
    {
        Controller.Update(Time.deltaTime);
    }

    public void OnMouseDown()
    {
        Controller.HideTowerUIs();
        ToggleUI(uiIsShown = !uiIsShown);
    }

    public void Shoot(MonsterView monster, ProjectileController controller) 
    {
        Vector3 pos = controller.View.transform.position;
        controller.View.transform.position = Utils.GetTopPoint(gameObject, -0.5f);
        TargetFollower follower = controller.View.GetComponent<TargetFollower>();
        follower.Target = monster.gameObject;
    }

    public void UpdateState(TowerViewModel viewModel)
    {
        if (viewModel.IsSold)
        {
            FlyingTextSpawner.SpawnGoldEarned(viewModel.SellPrice, gameObject);
            Destroy(gameObject);
            return;
        }

        if (ammoBar == null)
        {
            attacher = GetComponent<PrefabAttacher>();
            attacher.AttachPrefab();
            ammoBar = attacher.GetInstance().transform.Find("Ammunition Bar").GetComponent<Bar>();
            ammoBar.pointsImage
                = attacher.
                GetInstance().
                transform.Find("Ammunition Bar/Ammo").
                GetComponent<Image>();
            disabler = GetComponent<ObjectDisabler>();
            GameObject buttons = ammoBar.gameObject.transform.parent.Find("Buttons").gameObject;
            disabler.toBeDisabled = buttons;
            disabler.Disable();

            reloadButton = buttons.transform.Find("Reload").GetComponent<Button>();
            sellButton = buttons.transform.Find("Sell").GetComponent<Button>();

            reloadButton.onClick.AddListener(OnReload);
            sellButton.onClick.AddListener(OnSell);
        }

        range = viewModel.Range;
        ammoBar.UpdateState(viewModel.Ammo, viewModel.MaxAmmo);

        int ammoPercentage = (int)(viewModel.Ammo / viewModel.MaxAmmo * 100);
        if (ammoPercentage != lastAmmoPercentageShown)
        {
            lastAmmoPercentageShown = ammoPercentage;
            if (ammoPercentage < nextMilestone)
            {
                FlyingTextSpawner.SpawnAmmunitionLeft(ammoPercentage, gameObject);
                nextMilestone -= 100;
            }
        }
        
        if (viewModel.ReloadPrice != 0)
        {
            FlyingTextSpawner.SpawnGoldSpent(viewModel.ReloadPrice, gameObject);
        }
    }

    public MonsterView[] GetMonstersInRange()
    {
        List<MonsterView> monsters = new List<MonsterView>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            GameObject gameObject = collider.gameObject;
            MonsterView monster = gameObject.GetComponent<MonsterView>();
            if (monster != null) monsters.Add(monster);
        }

        return monsters.ToArray();
    }

    private void OnReload()
    {
        Controller.OnReload();
        nextMilestone = INITIAL_MILESTONE;
    }

    private void OnSell()
    {
        Controller.OnSell();
    }

    public void ToggleUI(bool on)
    {
        if (on)
        {
            disabler.Disable();
            FocusUI(false);
        }
        else
        {
            disabler.Enable();
            FocusUI(true);
        }
        uiIsShown = on;
    }

    private void FocusUI(bool shouldFocus)
    {
        if (scaleRelativeToCamera == null)
        {
            scaleRelativeToCamera
                = transform.Find("Tower UI(Clone)")
                            .GetComponent<ScaleRelativeToCamera>();
        }

        if (shouldFocus)
        {
            scaleRelativeToCamera.enabled = true;
        }
        else
        {
            scaleRelativeToCamera.enabled = false;
            scaleRelativeToCamera.ResetScale();
        }

    }
}
