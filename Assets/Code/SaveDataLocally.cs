using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SaveDataLocally : MonoBehaviour
{
    public InputField nameField;
    public Button submitButton;
    public Text txtScore;
   
    private int maxNumCharactersForName = 20;


    private void Start()
    {
        submitButton.onClick.AddListener(SavePlayer);
    }
    private void Update()
    {
        VerifyInput();
    }
   
    private void SavePlayer()
    {
        int score = int.Parse(txtScore.text);
        //CONECTAR
        string connection = "URI=file:" + Application.persistentDataPath + "/GolfDatabase";
        //string connection = "URI=file:Assets\\DB\\GolfDatabase";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        IDbCommand dbcmd;
        IDataReader reader;

        //CREAR TABLA
        dbcmd = dbcon.CreateCommand();
        string q_createTable = "CREATE TABLE IF NOT EXISTS players (id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(20) NOT NULL ,points INTEGER NOT NULL DEFAULT '0', timestamp DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP );";

        dbcmd.CommandText = q_createTable;
        reader = dbcmd.ExecuteReader();

        //VER SI EXISTE LA FILA
        string username = nameField.text;
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "SELECT name FROM players WHERE name='" + username + "'";
        //cmnd.ExecuteNonQuery();
        reader = cmnd.ExecuteReader();
        if (reader != null) 
        {
            //INSERTAR DATOS
            cmnd = dbcon.CreateCommand();
            cmnd.CommandText = "INSERT INTO players (name, points) VALUES ('" + username + "', " + score + ")";
            cmnd.ExecuteNonQuery();
            Debug.Log("usuario guardado");
        }
        else
        {
            cmnd = dbcon.CreateCommand();
            cmnd.CommandText = "UPDATE players SET points ="+ score + " WHERE name='" + username + "'"; 
            cmnd.ExecuteNonQuery();
            Debug.Log("usuario ya existe!");
            
        }

        //cerramos conexion
        reader.Close();
        reader = null;
        cmnd = null;
        dbcon.Close();
        dbcon = null;

        LevelSelector.instance.goHome();
    }
    public void VerifyInput()
    {
        submitButton.interactable = (nameField.text.Length < maxNumCharactersForName && nameField.text.Length > 0); //si te pasas del limite de caracteres el submitButton se bloquea.
    }
}