using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ISpeed : IComparable<ISpeed>
{
    int speed { get; }
}
