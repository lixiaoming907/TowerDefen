using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class CancelCollider : MonoBehaviour{

	[MenuItem ("XixoaMingMenu/CancelAllCollider")]
	static void CancelAllCollider () {
		GameObject[] pAllObjects = (GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject)); 
		List<GameObject> pReturn = new List<GameObject>(); 
		foreach (GameObject pObject in pAllObjects) 
		{ 
			if (pObject.hideFlags == HideFlags.NotEditable) 
			{ 
				continue; 
			} 
			if (pObject.hideFlags == HideFlags.HideAndDontSave) 
			{  
				continue; 
			} 
			
			pReturn.Add(pObject); 
		} 
		
		foreach (GameObject value in pReturn) 
		{ 
			Collider collider = value.GetComponent<Collider>();
			if (collider != null) {
				DestroyImmediate(collider);
//				Debug.Log("33333333333333333-----" + value.name + "`s collider is being Destroy");
			}
		} 
		pReturn.Clear(); 
		pReturn = null;
	}
}
