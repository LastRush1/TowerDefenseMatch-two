﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu]
public class Sphere2Main : BasicInfoTower
{
    [SerializeField]
    List<Sphere2> towers = new List<Sphere2>();
    public List<Sphere2> Towers
    {
        get { return towers; }
    }
}
