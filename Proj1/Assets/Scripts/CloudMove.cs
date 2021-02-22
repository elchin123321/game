using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloudMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CloudMoving();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CloudMoving()
    {
        var Seq = DOTween.Sequence();
        Seq.Append(transform.DOMoveX(transform.position.x + 10, 20));
        Seq.Append(transform.DOMoveX(transform.position.x, 20));
        Seq.SetLoops(-1);

    }
}
