using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarcodeManager : MonoBehaviour
{
  public static BarcodeManager Instance;
  public List<string> History;
  public bool wasDetected = false;
  
  private void Awake()
  {
    if (Instance != null)
    {
      return;
    }
    else
    {
      Instance = this;
    }
  }
}
