using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataReciever <T>
{
    void RecieveData(T data);
}
