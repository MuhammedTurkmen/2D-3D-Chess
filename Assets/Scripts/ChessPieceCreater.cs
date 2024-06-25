using System.Collections.Generic;
using UnityEngine;

public class ChessPieceCreater : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Whites, Blacks;

    [SerializeField]
    private Transform piecesParent;

    public Transform[] whitePiecesPos;
    public Transform[] blackPiecesPos;

    private List<SpriteRenderer> pieceSpriteRends = new List<SpriteRenderer>();

    private void Awake()
    {
        whitePiecesPos = new Transform[Whites.Length];
        blackPiecesPos = new Transform[Blacks.Length];

        CreateWhitePieces();
        CreateBlackPieces();
    }

    private void CreateWhitePieces()
    {  
        whitePiecesPos = CreateChessPieces(

                            Whites,
                            ChessPieceData.whiteStartX,
                            ChessPieceData.whiteStartZ,
                            ChessPieceData.oneCellWidth,
                            ChessPieceData.oneCellHeight

                        );
    }

    private void CreateBlackPieces()
    {
        blackPiecesPos = CreateChessPieces(

                            Blacks,
                            ChessPieceData.blackStartX,
                            ChessPieceData.blackStartZ,
                            ChessPieceData.oneCellWidth,
                            -ChessPieceData.oneCellHeight

                        );
    }

    private Transform[] CreateChessPieces(GameObject[] array, float startX, float zPos, float stepX, float stepZ)
    {
        Transform[] positions = new Transform[array.Length];

        byte line = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if (i % 8 == 0 && i != 0)
                line++;

            GameObject piece = Instantiate(array[i],piecesParent);

            pieceSpriteRends.Add(piece.transform.Find("PieceIcon").GetComponent<SpriteRenderer>());

            Vector3 piecePos = new Vector3(

                        startX + stepX * (i % 8),
                        ChessPieceData.chessPieceHeight,
                        zPos + line * stepZ
                
                        );

            piece.transform.position = piecePos;
            positions[i] = piece.transform;
        }

        Debug.Log(pieceSpriteRends.Count);
        return positions; 
    }

    public void FlipSprites()
    {
        for (int i = 0; i < pieceSpriteRends.Count; i++)
        {
            pieceSpriteRends[i].flipY = !pieceSpriteRends[i].flipY;

        }
    }
}
