using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class data 
{
    public static data Instance = new data();
    public Dictionary<string, skill> Dic_Skill = new Dictionary<string, skill>();

    public struct skill
    {
        public string name;
        public float damage;
    }

	

}
