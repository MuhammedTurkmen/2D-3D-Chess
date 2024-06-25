using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameTypes gameType;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private CameraSwitcher cameraSwitcher;

    [SerializeField]
    private ChessPieceCreater pieceCreater;

    public void SetPlayerTurn()
    {
        if (gameType == GameTypes.OfflinePlayer)
        {
            if (playerController.playerPieceColor == PieceColor.White)
                playerController.playerPieceColor = PieceColor.Black;
            else
                playerController.playerPieceColor = PieceColor.White;

            cameraSwitcher.SetRotations();
            pieceCreater.FlipSprites();
        }
    }
}

public enum GameTypes
{
    OfflinePlayer,
    OfflinePC,
    Online
}