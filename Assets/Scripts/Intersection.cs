using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection : MonoBehaviour
{
    int turnCount;
    
    [System.Serializable] public struct Turn
    {
       public TurnEnum turn;
       public GameObject path;    
    }

    [SerializeField] public Turn[] turns;

    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPath(int nextTurnInt)
    {
        /*
        foreach( Turn turn in turns)
        {
            if (turn.turn == nextTurn)
                return turn.path;
        }

        return null;
        */
        return turns[nextTurnInt].path;
    }

    public TurnEnum GetTurn(int nextTurnInt)
    {
        return turns[nextTurnInt].turn;
    }

    public int GetTurnCount()
    {
        return turns.Length;
    }

}
