using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPuntaje : MonoBehaviour
{
    private int puntaje = 0;
    public Text PuntajeC;

    public int GetPoint()
    {
        return puntaje;
    }
    public void AddPoints(int puntaje)
    {
        this.puntaje += puntaje;
        PuntajeC.text = "Puntaje: " + GetPoint();
    }
}
