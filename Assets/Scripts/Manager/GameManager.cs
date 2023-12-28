using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveManager
{
    public static GameManager instance;
    [SerializeField] private Checkpoint[] checkpoints;
    [SerializeField] private CinemachineConfiner confiner;
    [SerializeField] private Collider2D bossPhasecameraBounding;
    public string closestCheckpoing;
    private Boss boss;

    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, bool> pair in _data.checkpoints)
        {
            foreach (Checkpoint checkpoint in checkpoints)
            {
                if (checkpoint.checkpointId == pair.Key)
                {
                    if (pair.Value == true)
                    {
                        checkpoint.ActivateCheckpoint();
                    }
                }
            }
        }
        closestCheckpoing = _data.cloestCheckpointId;
        Debug.Log(checkpoints.Length);
        Invoke("PlacePlayerToClosetCheckPoint", .05f);
    }

    private void PlacePlayerToClosetCheckPoint()
    {
        foreach (Checkpoint check in checkpoints)
        {
            if (closestCheckpoing == check.checkpointId)
            {
                PlayerManager.instance.player.transform.position = check.transform.position;
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        if (FindClosestCheckpoint() != null)
        {
            _data.cloestCheckpointId = FindClosestCheckpoint().checkpointId;
        }
        _data.checkpoints.Clear();
        foreach (Checkpoint checkpoint in checkpoints)
        {
            _data.checkpoints.Add(checkpoint.checkpointId, checkpoint.activated);
        }
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        checkpoints = FindObjectsOfType<Checkpoint>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
    }
    private void Update()
    {
        if (boss.startBossCombat)
            confiner.m_BoundingShape2D = bossPhasecameraBounding;
    }

    public void RestartScene()
    {
        SaveManager.instance.SaveGame();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private Checkpoint FindClosestCheckpoint()
    {
        float closestDistance = Mathf.Infinity;
        Checkpoint cloestCheckpoint = null;
        foreach (var item in checkpoints)
        {
            float distance = Vector2.Distance(PlayerManager.instance.player.transform.position, item.transform.position);
            if (distance < closestDistance && item.activated == true)
            {
                closestDistance = distance;
                cloestCheckpoint = item;
            }
        }
        return cloestCheckpoint;
    }
}
