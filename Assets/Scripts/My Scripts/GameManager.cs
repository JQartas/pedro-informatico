﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

public class GameManager : MonoBehaviour
{
    private static int currentScore = 0;
    public static int CurrentLevel = 1;
    public static string directory = Application.streamingAssetsPath + "/Config/";
    public static string GameThematic = "​";
    // private string thematic;
    private static AssetBundle myAssetBundle;
    public static string[] scenePaths;
    public static string[] files;
    private static string[] questionsFile;
    public static bool isMusicPlaying = false;
    public static string lastLvl = "Level_1";
    private static List<QuestionClass> questions = new List<QuestionClass>();
    
    static GameManager() {
        System.IO.Directory.CreateDirectory(directory);
        files = Directory.GetFiles(directory, "*.txt");
        if (GameThematic != "")
        {
            FormatQuestions();
        }
    }

    public static string GetGameThematic()
    {
        return GameThematic;
    }

    public static List<QuestionClass> GetQuestions()
    {
        return questions;
    }

    public static void SetGameThematic(string newThematic)
    {
        GameThematic = newThematic;
    }

    public static void UpdateFiles()
    {
        files = Directory.GetFiles(directory, "*.txt");
    }

    public static void FormatQuestions() 
    {
        //Obtener archivo que contiene las preguntas
        var match = files.FirstOrDefault(stringToCheck => stringToCheck.Contains(GameThematic));
        //Se guarda en un array cada linea del archivo
        questions.Clear();
        if (match != null)
        {
            Debug.Log(match);
            questionsFile = File.ReadAllLines(match);
            Debug.Log(questionsFile);
            for (int i = 0; i < questionsFile.Length; i++)
            {
                Debug.Log("OLA");
                questions.Add(new QuestionClass(
                    i,
                    questionsFile[i].Substring(0, questionsFile[i].IndexOf("%%")),
                    //Array de respuestas
                    Format(questionsFile[i].Substring(questionsFile[i].IndexOf("%%"))),
                    //Respuesta correcta
                    1));
            }
        }
    }
 
    private static string[] Format(string stringLine) {
        //Formatear cada respuesta
		return stringLine.Split("%%%%".ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
    }

    public static int GetCurrentScore() {
        return currentScore;
    }

    public static void IncreaseScore() {
        currentScore++; 
    }

    public static void DecreaseScore()
    {
        currentScore--;
    }
}
