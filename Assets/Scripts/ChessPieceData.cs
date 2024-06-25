using UnityEngine;

public static class ChessPieceData
{
    public static Vector3[] rookMoves = {

        new(1, 0,0),   // sað
        new(-1, 0, 0), // sol
        new(0, 0, 1),  // üst
        new(0, 0, -1)  // alt

    };

    public static Vector3[] rookAttacks = {

        new(1, 0,0),   // sað
        new(-1, 0, 0), // sol
        new(0, 0, 1),  // üst
        new(0, 0, -1)  // alt

    };

    public static Vector3[] knightMoves = {

        new(2, 0,1),    // sað üst
        new(2, 0,-1),  // sað alt
        new(-2, 0, 1),  // sol üst
        new(-2,0, -1), // sol alt
        new(1, 0,2),   // üst sað
        new(-1,0, 2),  // üst sol
        new(1, 0,-2),  // alt sað
        new(-1,0, -2)  // alt sol

    };

    public static Vector3[] knightAttacks = {

        new(2,0,1),    // sað üst
        new(2, 0,-1),  // sað alt
        new(-2,0, 1),  // sol üst
        new(-2,0, -1), // sol alt
        new(1, 0,2),   // üst sað
        new(-1,0, 2),  // üst sol
        new(1, 0,-2),  // alt sað
        new(-1,0, -2)  // alt sol

    };

    public static Vector3[] bishopMoves = {

        new(1,0,1),    // sað üst çapraz
        new(1,0, -1),  // sað alt çapraz
        new(-1,0, 1),  // sol üst çapraz
        new(-1,0, -1)  // sol alt çapraz

    };

    public static Vector3[] bishopAttacks = {

        new(1,0,1),    // sað üst çapraz
        new(1,0, -1),  // sað alt çapraz
        new(-1,0, 1),  // sol üst çapraz
        new(-1,0, -1)  // sol alt çapraz

    };

    public static Vector3[] queenMoves = {

        new(1,0,0),     // sað
        new(-1,0,0),    // sol
        new(0,0, 1),    // üst
        new(0,0, -1),   // alt
        new(1,0,1),     // sað üst çapraz
        new(1,0, -1),   // sað alt çapraz
        new(-1,0, 1),   // sol üst çapraz
        new(-1,0, -1)   // sol alt çapraz

    };

    public static Vector3[] queenAttacks = {

        new(1,0,0),     // sað
        new(-1,0,0),    // sol
        new(0,0, 1),    // üst
        new(0,0, -1),   // alt
        new(1,0,1),     // sað üst çapraz
        new(1,0, -1),   // sað alt çapraz
        new(-1,0, 1),   // sol üst çapraz
        new(-1,0, -1)   // sol alt çapraz

    };

    public static Vector3[] kingMoves = {

        new(1,0,0),    // sað
        new(-1,0,0),   // sol
        new(0,0,1),    // üst
        new(0,0,-1),   // alt 
        new(1,0,1),    // sað üst çapraz
        new(1,0,-1),   // sað alt çapraz
        new(-1,0,1),   // sol üst çapraz
        new(-1,0,-1),  // sol alt çapraz 

    };

    public static Vector3[] kingAttacks = {

        new(1,0,0),    // sað
        new(-1,0,0),   // sol
        new(0,0,1),    // üst
        new(0,0,-1),   // alt 
        new(1,0,1),    // sað üst çapraz
        new(1,0,-1),   // sað alt çapraz
        new(-1,0,1),   // sol üst çapraz
        new(-1,0,-1),  // sol alt çapraz 

    };

    public static Vector3[] pawnMoves = {

        new(0,0,1), // üst
        
    };

    public static Vector3[] pawnStartMoves = { // hem saldýrý hem hareket

        new(0,0,2), // baþlangýç

    };

    public static Vector3[] pawnAttacks = {

        new(1,0,1), // sað üst yeme
        new(-1,0,1), // sol üst yeme

    };

    public static float whiteStartX = 1.75f;
    public static float whiteStartZ = 1.75f;

    public static float blackStartX = 1.75f;
    public static float blackStartZ = 12.25f;

    public static float oneCellWidth = 1.5f;
    public static float oneCellHeight = 1.5f;

    public static float chessPieceHeight = 0.02f;

    public static Vector3 centerOfBoard = new(7f, 0, 7f);
}

public enum ChessPieceType
{
    Rook,
    Knight,
    Bishop,
    Queen,
    King,
    Pawn
}

public enum PieceColor
{
    White,
    Black
}