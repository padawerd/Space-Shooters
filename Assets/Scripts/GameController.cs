﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	private int score = 0;
	private bool gameOver;
	private bool restart;

	void Start() {
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		StartCoroutine(SpawnWaves());
		UpdateScoreText();
	}

	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds(startWait);
		while (!gameOver) {
			for (int i = 0; i < hazardCount; ++i) {
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
		}
	}

	public void AddScore(int toBeAdded) {
		score += toBeAdded;
		UpdateScoreText();
	}

	public void GameOver() {
		gameOverText.text = "Game Over";
		gameOver = true;
		restartText.text = "Press 'R' to Restart";
		restart = true;
	}

	void UpdateScoreText() {
		scoreText.text = "Score: " + score.ToString();
	}

	void Update() {
		if (restart) {
			if (Input.GetKeyDown(KeyCode.R)) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}

}
