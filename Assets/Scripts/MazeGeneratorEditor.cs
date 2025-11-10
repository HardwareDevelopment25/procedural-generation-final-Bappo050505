using UnityEngine;
using UnityEditor;
using System.Runtime.CompilerServices;

[CustomEditor(typeof(MazeGenerator))]
public class MazeGeneratorEditor : Editor
{
    // Override the default Inspector GUI Rendering

    public override void OnInspectorGUI()
    {
        // Cast the target object to MazeGenerator so we can access its fields and methods
        MazeGenerator mazeGen = (MazeGenerator)target;
        GUILayout.Label("---Configure Maze---", EditorStyles.largeLabel);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // Draw the default inspector UI for all serialised fields
        if (DrawDefaultInspector())
        {
            //switches to 2D and mazkes new maze;

            if (mazeGen.size < 16 || mazeGen.size > 1000)
            {
                mazeGen.size = 32;
            }
            mazeGen.imageOfMaze();
        }

        //Add a button to do something, regenerate.

        if (GUILayout.Button(" GENERATE A NEW MAZE "))
        {
            mazeGen.imageOfMaze();
        }

    }
}

//define a custom editor window for generating complete game object

public class MazeGeneratorWindow : EditorWindow
{
    public int InitialMazeSize = 32;
    //generates menu item across whole project
    [MenuItem("Tools/Generate Maze By Size")]

    public static void showWindow()
    {
        GetWindow<MazeGeneratorWindow>();
    }


    private void OnGUI()
    {

        EditorGUILayout.Space();
        GUILayout.Label("Generate a maze in your scene");

        EditorGUILayout.Space();
        GUILayout.Label("Configure Maze:");

        //creates an input field in case we want to choose defualt maze size
        InitialMazeSize = EditorGUILayout.IntField("size", InitialMazeSize);


        //generate maze on button press
        if (GUILayout.Button("Generate"))
        {
            //create game object as a plane to put material on
            GameObject newGameObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
            newGameObject.name = (InitialMazeSize + "x" + InitialMazeSize + "Generated Maze");

            //undo creation of game object
            Undo.RegisterCreatedObjectUndo(newGameObject, "Undo Maze");

            //create material and shader
            Material material = new Material(Shader.Find("Unlit/Texture"));
            //material.color = Color.white;


            //add scrip to generate maze
            //adding a component also returnes a reference
            MazeGenerator mazeGen =  newGameObject.AddComponent<MazeGenerator>();



            //get the meshrenderer on the material and run the generation of the maze

            newGameObject.GetComponent<MeshRenderer>().material = material;

            mazeGen.size = InitialMazeSize;
            mazeGen.imageOfMaze();


        }

    }
}

