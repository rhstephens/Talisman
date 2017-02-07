﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Singleton class for use of general functions across multiple scenes.
/// </summary>
public class GameManager : MonoBehaviour {
    //
    // Publics
    //
    public GameObject tilePrefab;

    //
    // Privates
    //
    private static GameManager _instance;
    private Tile[,] board;

    // disable use of regular constructor
    private GameManager () { }

    /// <summary>
    /// Get the instance of the GameManager
    /// </summary>
    /// <returns></returns>
	public static GameManager instance {
        get {
            if (_instance == null) {
                _instance = (new GameObject("GMContainer").AddComponent<GameManager>());
            }
            return _instance;
        }
    }

    /// <summary>
    /// Generates a board that fits within the given constraints. startLoc is the centre of the board
    /// </summary>
    public void generateBoard(float width, float height, int x, int y, Vector2 center) {
        float tileHeight = height / y;
        float tileWidth = tileHeight;

        // create a template for the tiles
        GameObject container = new GameObject("GridContainer");
        container.transform.position = center;

        // create a 2d array of Tiles
        for (int i = -(x / 2); i < Mathf.CeilToInt(x / 2.0f); i++) {
            for (int j = -(y / 2); j < Mathf.CeilToInt(y / 2.0f); j++) {
                Vector3 pos = new Vector3(center.x + (i * tileWidth + (((x + 1) % 2) * (tileWidth / 2))),
                   center.y + (j * tileHeight + (((y + 1) % 2) * (tileHeight / 2))), 0);

                GameObject obj = (GameObject) Instantiate(tilePrefab, container.transform);
                obj.name = "tile(" + (j + Mathf.CeilToInt(y / 2.0f)) + "," + (i + x / 2) + ")";
                obj.transform.position = pos;
                BoxCollider2D col = obj.GetComponent<BoxCollider2D>();
                col.size = new Vector2(tileWidth, tileHeight);
                
            }
        }
    }

    // For testing purposes. Generates a board that fills the whole camera field of view
    public void generateBoardFromCamera(int x, int y) {
        Camera cam = Camera.main;
        float height = cam.orthographicSize * 2f;
        float width = cam.aspect * height;
        
        generateBoard(width, height, x, y, cam.transform.position);
    }

    void Awake() {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
