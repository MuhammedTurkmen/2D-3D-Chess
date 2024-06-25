using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    [Header("Taþ Rengi")]
    public PieceColor pieceColor;

    [Header("Taþ Tipi")]
    public ChessPieceType pieceType;

    [Header("Taþ Ayarlarý")]
    public bool startMove = false, moveForward, endlessMove, selected, canMove;

    // 1.75 - 12.25 arasý

    public Vector3[] moves, startMoves, attackMoves;

    private PlayerController playerControl;
    private SpriteRenderer spriteRend;
    private Transform pointer;
    private Vector3 startPos;

    private void Awake()
    {
        switch (pieceType)
        {
            case ChessPieceType.Bishop:
                moves = ChessPieceData.bishopMoves;
                attackMoves = ChessPieceData.bishopAttacks;
                break;
            case ChessPieceType.King:
                moves = ChessPieceData.kingMoves;
                attackMoves = ChessPieceData.kingAttacks;
                break;
            case ChessPieceType.Knight:
                moves = ChessPieceData.knightMoves;
                attackMoves = ChessPieceData.knightAttacks;
                break;
            case ChessPieceType.Pawn:
                moves = ChessPieceData.pawnMoves;
                startMoves = ChessPieceData.pawnStartMoves;
                attackMoves = ChessPieceData.pawnAttacks;
                break;
            case ChessPieceType.Queen:
                moves = ChessPieceData.queenMoves;
                attackMoves = ChessPieceData.queenAttacks;
                break;
            case ChessPieceType.Rook:
                moves = ChessPieceData.rookMoves;
                attackMoves = ChessPieceData.rookAttacks;
                break;
        }

        playerControl = GameObject.Find("3D Player Camera").GetComponent<PlayerController>();
        spriteRend = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        //if (pieceColor == PieceColor.Black && moveForward)
        //{
        //    for (int i = 0; i < moves.Length; i++)
        //    {
        //        moves[i].z = -moves[i].z;
        //    }

        //    for (int i = 0; i < startMoves.Length; i++)
        //    {
        //        startMoves[i].z = -startMoves[i].z;
        //    }

        //    for (int i = 0; i < attackMoves.Length; i++)
        //    {
        //        attackMoves[i].z = -attackMoves[i].z;
        //    }
        //}
    }

    public void ShowPieceMoves()
    {
        if (playerControl.playerPieceColor == pieceColor)
        {
            playerControl.chessPiece = this;
            playerControl.ShowPossibleMoves(moves, startMoves, attackMoves);
        }
    }

    private void MovePiece(Vector3 movePosition)
    {
        playerControl.MovePiece(movePosition);
    }

    private void OnMouseDown()
    {
        if (pieceColor != playerControl.playerPieceColor)
            return;

        ShowPieceMoves();

        if (!canMove)
            return;

        Cursor.visible = false;
        startPos = transform.position;

        spriteRend.sortingOrder = 50;

        selected = true;
    }

    private void OnMouseDrag()
    {
        if (selected)
        {
            float zCoord = Camera.main.WorldToScreenPoint(transform.position).z;

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = zCoord;

            Vector3 pos = Camera.main.ScreenToWorldPoint(mousePosition);
            pos.y = ChessPieceData.chessPieceHeight;

            transform.position = pos;
        }
    }

    private void OnMouseUp()
    {
        if (!selected)
            return;

        selected = false;

        spriteRend.sortingOrder = 0;

        if (pointer != null)
            MovePiece(pointer.position);
        else
            transform.position = startPos;

        Cursor.visible = true;
        pointer = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Pointer"))
            pointer = other.transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (pointer == null && other.transform.CompareTag("Pointer"))
            pointer = other.transform;

        if (pointer != null && other == null)
            pointer = null;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Pointer"))
            pointer = null;
    }
}