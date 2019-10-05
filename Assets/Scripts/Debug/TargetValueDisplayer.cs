using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetValueDisplayer : MonoBehaviour
{
    public MonsterTarget Target;

    // Update is called once per frame
    void Update()
    {
        print(Target.CalculateInterest(transform.position));
    }
}
