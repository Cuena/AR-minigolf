using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;

public class GetRanking : MonoBehaviour
{

    private List<Jugador> rankingJugadores = new List<Jugador>();
    private string[] CurrentArray = null;

    public Transform tfPanelCargarDatos;
    public Text txtCargando;
    public GameObject PanelPre;


    private void Start()
    {
        ObtenerJugadores();
    }


    void ObtenerJugadores()
    {
        txtCargando.text = "Cargando...";
        //CONECTAR
        string connection = "URI=file:" + Application.persistentDataPath + "/GolfDatabase";
        //string connection = "URI=file:Assets\\DB\\GolfDatabase";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        IDbCommand dbcmd;
        IDataReader reader;

        //VER SI EXISTE LA FILA
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "SELECT name, points FROM players ORDER BY points ASC LIMIT 10"; //igual no funciona esto
        //cmnd.ExecuteNonQuery();
        reader = cmnd.ExecuteReader();
        txtCargando.text = "";
        while (reader.Read())
        {
            rankingJugadores.Add(new Jugador(reader[0].ToString(),reader[1].ToString()));
        }

        //cerramos conexion
        dbcon.Close();
        //pintamos el resultado
        verRegistros();
    }

    void verRegistros() //sacarlos por pantalla
    {
        for (int i = 0; i < rankingJugadores.Count; i++)
        {
            GameObject obj = Instantiate(PanelPre);
            Jugador j = rankingJugadores[i];
            obj.GetComponent<setScore>().SetScore(j.nombre, j.puntuacion);
            obj.transform.SetParent(tfPanelCargarDatos);
            obj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }
}


public class Jugador
{

    public string nombre, puntuacion;
    public Jugador(string nombrejugador, string puntuacionjugador)
    {
        puntuacion = puntuacionjugador;
        nombre = nombrejugador;
    }

}