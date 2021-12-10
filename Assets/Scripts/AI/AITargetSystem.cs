using System;
using UnityEngine;

[ExecuteInEditMode]
public class AITargetSystem : MonoBehaviour
{
    AISensoryMemory sensoryMemoryPlayers = new AISensoryMemory(10, "PlayerLayer");
    AISensoryMemory sensoryMemoryBases = new AISensoryMemory(3, "BaseLayer");
    AISensor sensor;
    AIMemory bestMemoryPlayer;
    AIMemory bestMemoryBase;
    public float memorySpan = 3.0f;
    [Range(0, 1)]
    public float distanceWeight = 1.0f;
    [Range(0, 1)]
    public float angleWeight = 1.0f;
    [Range(0, 1)]
    public float ageWeight = 1.0f;

    public bool HasTargetPlayer
    {
        get
        {
            return bestMemoryPlayer != null;
        }
    }

    public GameObject TargetPlayer
    {
        get
        {
            return bestMemoryPlayer.gameObject;
        }
    }

    public Vector3 TargetPlayerPosition
    {
        get
        {
            return bestMemoryPlayer.posiiton;
        }
    }

    public bool TargetPlayerInSight
    {
        get
        {
            return bestMemoryPlayer != null && bestMemoryPlayer.Age < 0.5f;
        }
    }

    public float TargetPlayerDistance
    {
        get
        {
           return bestMemoryPlayer.distance;
        }
    }

    public void Start()
    {
        sensor = GetComponent<AISensor>();
    }

    void EvaluateScores()
    {
        foreach(var memory in sensoryMemoryPlayers.memories)
        {
            if(!Configuration.OppositeTeams(Configuration.RetreivePlayer(memory.gameObject), Configuration.RetreivePlayer(gameObject)))
            {
                continue;
            }
            memory.score = CalculateScore(memory);
            if (bestMemoryPlayer == null || memory.score > bestMemoryPlayer.score)
            {
                bestMemoryPlayer = memory;
            }
        }

        foreach (var memory in sensoryMemoryBases.memories)
        {
            memory.score = CalculateScore(memory);
            if (bestMemoryBase == null || memory.score > bestMemoryBase.score)
            {
                bestMemoryBase = memory;
            }
        }
    }

    float Normalize(float value, float maxValue)
    {
        return 1.0f - (value / maxValue);
    }

    float CalculateScore(AIMemory memory)
    {
        float distanceScore = Normalize(memory.distance , sensor.distance) * distanceWeight;
        float angleScore = Normalize(memory.angle , sensor.angle) * distanceWeight;
        float ageScore = Normalize(memory.Age , memorySpan) * ageWeight;
        return distanceScore + angleScore + ageScore;
    }

    public void Update()
    {
        sensoryMemoryPlayers.UpdateSenses(sensor);
        sensoryMemoryBases.UpdateSenses(sensor);

        sensoryMemoryPlayers.ForgetMemories(memorySpan);
        sensoryMemoryBases.ForgetMemories(memorySpan);
        EvaluateScores();
    }

    private void OnDrawGizmos()
    {
        float maxScore = float.MinValue;
        foreach (var memory in sensoryMemoryPlayers.memories)
        {
            if(memory.score > maxScore)
            {
                maxScore = memory.score;
            }
        }
        foreach(var memory in sensoryMemoryPlayers.memories)
        {
            Color color = Color.red;
            if (memory == bestMemoryPlayer)
            {
                color = Color.yellow;
            }
            color.a = memory.score / maxScore;
            Gizmos.color = color;
            Gizmos.DrawSphere(memory.posiiton, 0.2f);
        }
    }
}
