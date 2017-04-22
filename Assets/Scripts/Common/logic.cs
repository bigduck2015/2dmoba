using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logic 
{

    public static GameObject Instantiate(string path_prefab, Transform father = null)
    {
        Object prefab = Resources.Load(path_prefab);
        GameObject obj = MonoBehaviour.Instantiate(prefab) as GameObject;
        obj.transform.parent = father;
        obj.transform.localPosition = (prefab as GameObject).transform.localPosition;
        obj.transform.localRotation = (prefab as GameObject).transform.localRotation;
        obj.transform.localScale = (prefab as GameObject).transform.localScale;
        obj.name = prefab.name;
        return obj;
    }
	

}
