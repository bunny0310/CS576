using System;
using UnityEngine;

[ExecuteInEditMode]
public class AITargetSystem : MonoBehaviour
{
    AISensoryMemory sensoryMemory = new AISensoryMemory(10);
    AISensor sensor;
    AIMemory bestMemory;
    public float memorySpan = 3.0f;
    [Range(0, 1)]
    public float distanceWeight = 1.0f;
    [Range(0, 1)]
    public float angleWeight = 1.0f;
    [Range(0, 1)]
    public float ageWeight = 1.0f;

    public bool HasTarget
    {
        get
        {
            return bestMemory != null;
        }
    }

    public GameObject Target
    {
        get
        {
            return bestMemory.gameObject;
        }
    }

    public Vector3 TargetPosition
    {
        get
        {
            return bestMemory.posiiton;
        }
    }

    public bool TargetInSight
    {
        get
        {
            return bestMemory.Age < 0.5f;
        }
    }

    public float TargetDistance
    {
        get
        {
           return bestMemory.distance;
        }
    }

    public void Start()
    {
        sensor = GetComponent<AISensor>();
    }

    void EvaluateScores()
    {
        foreach(var memory in sensoryMemory.memories)
        {
            memory.score = CalculateScore(memory);
            if (bestMemory == null || memory.score > bestMemory.score)
            {
                bestMemory = memory;
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
        sensoryMemory.UpdateSenses(sensor);
        sensoryMemory.ForgetMemories(memorySpan);
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log($"{gameObject.name}'s memories - ");
            sensoryMemory.memories.ForEach(m =>
            {
                Debug.Log(m.Age);
            });
        }
        EvaluateScores();
    }

    private void OnDrawGizmos()
    {
        float maxScore = float.MinValue;
        foreach (var memory in sensoryMemory.memories)
        {
            if(memory.score > maxScore)
            {
                maxScore = memory.score;
            }
        }
        foreach(var memory in sensoryMemory.memories)
        {
            Color color = Color.red;
            if (memory == bestMemory)
            {
                color = Color.yellow;
            }
            color.a = memory.score / maxScore;
            Gizmos.color = color;
            Gizmos.DrawSphere(memory.posiiton, 0.2f);
        }
    }
}
