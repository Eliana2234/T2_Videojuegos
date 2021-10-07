using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlVida : MonoBehaviour
{
    private int Vidas = 3;
    public Text Vida;

    public int GetVida()
    {
        return Vidas;
    }
    public void QuitarVida(int Vidas)
    {
        this.Vidas -= Vidas;
        Vida.text = "Vidas: " + GetVida();
    }
}
