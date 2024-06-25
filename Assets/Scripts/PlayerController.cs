using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Color")]
    public PieceColor playerPieceColor;

    [SerializeField, Header("Ray Layers")]
    private LayerMask pieceLayerMask, pointerLayerMask;

    [SerializeField, Header("Pointers")]
    private GameObject[] greenCirclePointers, redCirclePointers;

    [SerializeField]
    private ChessPieceCreater pieceCreater;

    [SerializeField]
    private GameManager gameManager;

    private List<Vector3> moveList = new List<Vector3>();
    private List<Vector3> attackList = new List<Vector3>();

    public ChessPiece chessPiece;



    // byte  0 255
    // short 32.767 -32.768
    // int   2.147.483.647 -2.147.483.648

    // shows mouse position
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 mousePos = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            mousePos = hit.point;
        }

        mousePos.y = 0.75f;

        Gizmos.DrawSphere(mousePos, 0.3f);
    }

    public void ShowPossibleMoves(Vector3[] moves, Vector3[] startMoves, Vector3[] attacks)
    {
        moveList.Clear();
        attackList.Clear();

        for (int i = 0; i < moves.Length; i++)
        {
            if (chessPiece.endlessMove)
                SetEndlessMoves(moves, i);
            else
                SetMoves(moves, i);
        }

        for (int i = 0; i < startMoves.Length; i++)
        {
            if (chessPiece.startMove)
                SetMoves(startMoves, i);
        }

        for (int i = 0; i < attacks.Length; i++)
        {
            if (chessPiece.endlessMove)
                SetEndlessAttacks(attacks, i);
            else
                SetAttacks(attacks, i);
        }

        if (moveList.Count > 0 || attackList.Count > 0)
            chessPiece.canMove = true;
        else
            chessPiece.canMove = false;

        SetPointers();
    }

    private void SetMoves(Vector3[] moves, int i)
    {
        bool[] conditions;

        float xPos = chessPiece.transform.position.x + (moves[i].x * 1.5f);
        float zPos;

        if (chessPiece.pieceColor == PieceColor.Black && chessPiece.moveForward)
            zPos = chessPiece.transform.position.z - (moves[i].z * 1.5f);
        else
            zPos = chessPiece.transform.position.z + (moves[i].z * 1.5f);

        conditions =
            CheckCell(chessPiece, new Vector3(xPos, ChessPieceData.chessPieceHeight, zPos));

        if (xPos > 1.70f && xPos < 12.3f)
        {
            if (zPos > 1.70f && zPos < 12.3f)
            {
                if (!conditions[0])
                {
                    //Debug.Log("Moves : " + new Vector3(xPos, 0, zPos));

                    moveList.Add(new Vector3(xPos, ChessPieceData.chessPieceHeight, zPos));
                }
            }
        }
    }

    private void SetEndlessMoves(Vector3[] moves, int i)
    {
        bool[] conditions;

        byte k = 1;

        while (true)
        {
            float xPos = chessPiece.transform.position.x + (moves[i].x * 1.5f * k);
            float zPos;

            if (chessPiece.pieceColor == PieceColor.Black && chessPiece.moveForward)
                zPos = chessPiece.transform.position.z - (moves[i].z * 1.5f * k);
            else
                zPos = chessPiece.transform.position.z + (moves[i].z * 1.5f * k);

            conditions =
                CheckCell(chessPiece, new Vector3(xPos, ChessPieceData.chessPieceHeight, zPos));

            if (xPos > 1.70f && xPos < 12.3f)
            {
                if (zPos > 1.70f && zPos < 12.3f)
                {
                    if (!conditions[0])
                    {
                        //Debug.Log("Moves : " + new Vector3(xPos, 0, zPos));

                        moveList.Add(new Vector3(xPos, ChessPieceData.chessPieceHeight, zPos));
                        k++;
                    }
                    else
                        break;
                }
                else
                    break;
            }
            else
                break;
        }
    }

    private void SetAttacks(Vector3[] moves, int i)
    {
        bool[] conditions;

        float xPos = chessPiece.transform.position.x + (moves[i].x * 1.5f);
        float zPos;

        if (chessPiece.pieceColor == PieceColor.Black && chessPiece.moveForward)
            zPos = chessPiece.transform.position.z - (moves[i].z * 1.5f);
        else
            zPos = chessPiece.transform.position.z + (moves[i].z * 1.5f);

        conditions =
            CheckCell(chessPiece, new Vector3(xPos, ChessPieceData.chessPieceHeight, zPos));

        if (xPos > 1.70f && xPos < 12.3f)
        {
            if (zPos > 1.70f && zPos < 12.3f)
            {
                if (conditions[0] && conditions[1])
                {
                    //Debug.Log("Attack : " + new Vector3(xPos, 0, zPos));

                    attackList.Add(new Vector3(xPos, ChessPieceData.chessPieceHeight, zPos));
                }
            }
        }
    }

    private void SetEndlessAttacks(Vector3[] moves, int i)
    {
        bool[] conditions;

        byte pieceCount = 0;
        byte k = 1;

        while (true)
        {
            float xPos = chessPiece.transform.position.x + (moves[i].x * 1.5f * k);
            float zPos;

            if (chessPiece.pieceColor == PieceColor.Black && chessPiece.moveForward)
                zPos = chessPiece.transform.position.z - (moves[i].z * 1.5f * k);
            else
                zPos = chessPiece.transform.position.z + (moves[i].z * 1.5f * k);

            conditions = CheckCell(chessPiece, new Vector3(xPos, ChessPieceData.chessPieceHeight, zPos));

            if (xPos > 1.70f && xPos < 12.3f)
            {
                if (zPos > 1.70f && zPos < 12.3f)
                {
                    if (conditions[0] && conditions[1] && pieceCount < 1)
                    {
                        //Debug.Log("Attack : " + new Vector3(xPos, 0, zPos));

                        attackList.Add(new Vector3(xPos, ChessPieceData.chessPieceHeight, zPos));
                        pieceCount++;
                        k++;
                    }
                    else if (!conditions[0])
                        k++;
                    else
                        break;
                }
                else
                    break;
            }
            else
                break;
        }
    }

    private void SetPointers()
    {
        ClearPointers();

        for (int i = 0; i < moveList.Count; i++)
        {
            greenCirclePointers[i].transform.position = moveList[i];
            greenCirclePointers[i].SetActive(true);
        }

        for (int i = 0; i < attackList.Count; i++)
        {
            redCirclePointers[i].transform.position = attackList[i];
            redCirclePointers[i].SetActive(true);
        }
    }

    public void ClearPointers()
    {
        for (int i = 0; i < greenCirclePointers.Length; i++)
        {
            greenCirclePointers[i].SetActive(false);
        }
        for (int i = 0; i < redCirclePointers.Length; i++)
        {
            redCirclePointers[i].SetActive(false);
        }
    }

    private void Attack(Vector3 attackPos)
    {
        if (playerPieceColor == PieceColor.White)
        {
            for (int i = 0; i < pieceCreater.blackPiecesPos.Length; i++)
            {
                if (pieceCreater.blackPiecesPos[i].position == attackPos)
                {
                    pieceCreater.blackPiecesPos[i].gameObject.SetActive(false);
                }
            }
        }
        else if (playerPieceColor == PieceColor.Black)
        {
            for (int i = 0; i < pieceCreater.whitePiecesPos.Length; i++)
            {
                if (pieceCreater.whitePiecesPos[i].position == attackPos)
                {
                    pieceCreater.whitePiecesPos[i].gameObject.SetActive(false);
                }
            }
        }
    }

    private bool[] CheckCell(ChessPiece piece, Vector3 position)
    {
        bool[] _conditions = new bool[2];

        for (int i = 0; i < pieceCreater.whitePiecesPos.Length; i++)
        {
            if (pieceCreater.whitePiecesPos[i].gameObject.activeSelf && pieceCreater.whitePiecesPos[i].position == position)
            {
                _conditions[0] = true;

                if (piece.pieceColor == PieceColor.White)
                {
                    _conditions[1] = false;
                }
                else if (piece.pieceColor == PieceColor.Black)
                {
                    _conditions[1] = true;
                }
            }
        }

        for (int i = 0; i < pieceCreater.blackPiecesPos.Length; i++)
        {
            if (pieceCreater.blackPiecesPos[i].gameObject.activeSelf && pieceCreater.blackPiecesPos[i].position == position)
            {
                _conditions[0] = true;

                if (piece.pieceColor == PieceColor.White)
                {
                    _conditions[1] = true;
                }
                else if (piece.pieceColor == PieceColor.Black)
                {
                    _conditions[1] = false;
                }
            }
        }

        return _conditions;
    }

    public void MovePiece(Vector3 movePosition)
    {
        if (chessPiece.startMove)
            chessPiece.startMove = false;

        bool[] condition = CheckCell(chessPiece, movePosition);
        //Debug.Log("Cell Is Empty : " + condition[0] + " | Is Enemy : " + condition[1]);

        chessPiece.transform.position = movePosition;
        //Debug.Log("Move Position : " + movePosition);

        if (condition[0] && condition[1])
        {
            Debug.Log("Attacked Pos : " + movePosition);
            Attack(movePosition);
        }

        gameManager.SetPlayerTurn();

        ClearPointers();
    }
}