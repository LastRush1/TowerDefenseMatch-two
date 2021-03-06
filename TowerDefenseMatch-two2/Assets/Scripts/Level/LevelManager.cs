﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour
{
    BoardCreater boardCreater;
    TowerManager towerManager;
    EnemyController enemyController;
    TowerController towerController;
    AttackController attackController;
    FindTarget findTarget;

    [SerializeField]
    EnemyFactory enemyFactory;

    Ray TouchRay => Camera.main.ScreenPointToRay(Input.mousePosition);

    int GridNumber = 0;

    GridPlace gridPlace;

    Tower tower;

    private void Awake()
    {
        boardCreater = GetComponent<BoardCreater>();
        towerManager = GetComponent<TowerManager>();
        enemyController = GetComponent<EnemyController>();
        towerController = GetComponent<TowerController>();
        attackController = GetComponent<AttackController>();
        findTarget = GetComponent<FindTarget>();
    }
    void Start()
    {
        boardCreater.LoadMap(true);
        towerManager.SetTowers(boardCreater.GridPlaceList);
        enemyController.StartSpawn(boardCreater.FirstRoad);
        enemyController.RoadSet(boardCreater.Road);
        towerManager.SetTowerController = towerController;

        //enemyFactory.Get();
    }


    void Update()
    {
        enemyController.GameUpdate();
        attackController.EnemyData(enemyController.EnemyPos, enemyController.EnemyGridPos, enemyController.EnemytargetPoints);
        findTarget.GameUpdate();
        Physics.SyncTransforms();

        towerManager.UnionUpdate();
        towerController.GameUpdate();

        attackController.GameUpdate();
        enemyController.HitsChek();
        PlayModeActive();
    }


    /// <summary>
    /// Апдейт в PlayerMod'е
    /// </summary>
    void PlayModeActive()
    {
        if (!IsPointerOverUIObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                gridPlace = boardCreater.TryGetGrid(TouchRay);
                //BuildPlayerController(gridPlace);
                GridNumber = gridPlace.NumberGrid;
                tower = towerManager.TryTakeTower(GridNumber);
                if (tower != null)
                {
                    tower.transform.localScale = new Vector3(tower.transform.localScale.x * 2, tower.transform.localScale.y * 2, tower.transform.localScale.z * 2);
                }
                Debug.Log($"Номер тайла :{gridPlace.NumberGrid}");
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (tower != null)
            {
                tower.transform.localScale = new Vector3(tower.transform.localScale.x /2, tower.transform.localScale.y / 2, tower.transform.localScale.z / 2);
                gridPlace = boardCreater.TryGetGrid(TouchRay);
                int grid = GridNumber;
                GridNumber = gridPlace.NumberGrid;
                towerManager.PotentialUnion(grid,GridNumber);
                tower = null;
            }
        }
        //BuyAndBuildTower(GridNumber);
    }

    public void buttonClick()
    {
        towerManager.addTower();
    }



    /// <summary>
    /// Не дает кликать и на UI и на 2d объект(UI важнее)
    /// </summary>
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
#if !ANDROID
        eventDataCurrentPosition.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
#else
        eventDataCurrentPosition.position = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
#endif
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
