using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField]
    private float lerpTime, normalCamSize, topCamSize;

    [SerializeField, Header("Kamera Pozisyonlarý")]
    private Vector3 whitePlayerPos, blackPlayerPos, topDownPos;

    [SerializeField, Header("Iþýn Katmanlarý")]
    private LayerMask normalCamLayerMask, topCamLayerMask;

    [SerializeField, Header("Kamera Rotasyonlarý")]
    private Quaternion whitePlayerRot, blackPlayerRot, whitePlayerTopDownRot, blackPlayerTopDownRot;

    private bool topView, canTransformChange;
    private Vector3 targetPos;
    private Quaternion targetRot;

    private PlayerController playerController;
    private CameraTurn camTurn;
    private Camera normalCam;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        camTurn = GetComponent<CameraTurn>();
        normalCam = GetComponent<Camera>();
    }

    private void Start()
    {
        targetRot = whitePlayerRot;

        topView = true;
    }

    private void Update()
    {
        SwitchCamera();
    }

    private void SwitchCamera()
    {
        if (Input.GetKeyDown(KeyCode.W) && !canTransformChange)
        {
            SetCameraView();
        }

        CallTransition();
    }

    private void CallTransition()
    {
        if (canTransformChange)
            SetCamera(targetPos, targetRot);

        if (transform.position == topDownPos)
        {
            if ((playerController.playerPieceColor == PieceColor.White && transform.rotation == whitePlayerTopDownRot) || (transform.rotation == blackPlayerTopDownRot && playerController.playerPieceColor == PieceColor.Black))
            {
                normalCam.orthographic = true;

                normalCam.orthographicSize = topCamSize;
                normalCam.cullingMask = topCamLayerMask;

                canTransformChange = false;
            }
        }
        else if ((playerController.playerPieceColor == PieceColor.White && transform.position == whitePlayerPos && transform.rotation == whitePlayerRot) || (playerController.playerPieceColor == PieceColor.Black && transform.position == blackPlayerPos && transform.rotation == blackPlayerRot))
        {
            camTurn.enabled = true;
            canTransformChange = false;
        }
    }

    private void SetCameraView()
    {
        topView = !topView;

        SetRotations();
     
        if (topView)
        {
            camTurn.enabled = false;
        }
        else
        {
            normalCam.orthographic = false;

            normalCam.fieldOfView = normalCamSize;
            normalCam.cullingMask = normalCamLayerMask;
        }
    }

    public void SetRotations()
    {
        canTransformChange = true;

        if (topView)
        {
            targetPos = topDownPos;

            if (playerController.playerPieceColor == PieceColor.White)
            {
                targetRot = whitePlayerTopDownRot;
            }
            else if (playerController.playerPieceColor == PieceColor.Black)
            {
                targetRot = blackPlayerTopDownRot;
            }
        }
        else if (!topView)
        {
            if (playerController.playerPieceColor == PieceColor.White)
            {
                targetPos = whitePlayerPos;
                targetRot = whitePlayerRot;
            }
            else if (playerController.playerPieceColor == PieceColor.Black)
            {
                targetPos = blackPlayerPos;
                targetRot = blackPlayerRot;
            }
        }

        //Debug.Log("Position : " + targetPos);
        //Debug.Log("Rotation : " + targetRot);
    }

    private void SetCamera(Vector3 finalPos, Quaternion finalRot)
    {
        transform.position = Vector3.Lerp(transform.position, finalPos, lerpTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, finalRot, lerpTime);
    }
}
