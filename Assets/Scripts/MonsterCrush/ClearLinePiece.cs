using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearLinePiece : ClearblePiece {


    public bool isRow;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Clear()
    {
        base.Clear();

        if(isRow)
        {
            //Clear Row
            piece.GridRef.ClearRow(piece.Y);
        }
        else
        {
            //Clear Column
            piece.GridRef.ClearColumn(piece.X);
        }
    }
}
