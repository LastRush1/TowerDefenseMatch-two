﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : TowerContent
{
    TowerFactory originFactory;
    //int towerNum;

    [SerializeField]
    Text levelNumText = default;

    [SerializeField]
    Transform turret = default;

    public Transform Turret
    { 
        get { return turret; }
    }

    [SerializeField]
    GameObject body = default;

    public GameObject Body
    {
        get { return body; }
    }


    public void Initialize()
    {

    }

    void Start()
    {
        changeLevelText(level);
    }


    void Update()
    {
        
    }

    /// <summary>
    /// Вывод текста
    /// </summary>
    /// <param name="level"></param>
    public void changeLevelText(int level)
    {
        levelNumText.text = level.ToString();
    }

    public void LevelUp()
    {
        level++;
        changeLevelText(level);
    }


    public TowerFactory OriginFactory
    {
        get => originFactory;
        set
        {
            Debug.Assert(originFactory == null, "Redefined origin factory!");
            originFactory = value;
        }
    }


}
