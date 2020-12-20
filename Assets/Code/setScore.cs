using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setScore : MonoBehaviour
{
    public GameObject Nombre, Puntuacion;

    public void SetScore(string nombreJugador, string puntuacionJugador)
    {
        Nombre.GetComponent<UnityEngine.UI.Text>().text = nombreJugador;
        Puntuacion.GetComponent<UnityEngine.UI.Text>().text = puntuacionJugador;






    }
}
