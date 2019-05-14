using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    public float Horizontal;
    public float Vertical;
    public float Scroll;

    // For Unit Testing Only
    public void Construct(float horizontal, float vertical, float scroll)
    {
        this.Horizontal = horizontal;
        this.Vertical = vertical;
        this.Scroll = scroll;
    }
}
