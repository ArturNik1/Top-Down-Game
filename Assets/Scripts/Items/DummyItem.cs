using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyItem : Item
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    public override void PickUpItem()
    {
        GameObject.Find("Tutorial Manager").GetComponent<TutorialManager>().pickedUpItem = true;
    }
}
