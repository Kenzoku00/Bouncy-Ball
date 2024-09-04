// using UnityEngine;

// public class StoneController : MonoBehaviour
// {
//     [SerializeField] private Material hoverMaterial;
//     [SerializeField] private Material defaultMaterial;
//     [SerializeField] private CameraControl cameraControl;

//     [SerializeField] private float moveSpeed = 5f;
//     [SerializeField] private bool sudahSampai;

//     private Vector3 offset;
//     private bool isDragging = false;
//     private PipeController currentPipe;
//     private Renderer stoneRenderer;
//     private Vector3 targetPosition;
//     private bool isMoving = false;

//     private void Awake()
//     {
//         stoneRenderer = GetComponent<Renderer>();
//         if (stoneRenderer != null)
//         {
//             stoneRenderer.material = defaultMaterial;
//         }
//     }

//     private void OnMouseEnter()
//     {
//         if (!isDragging && cameraControl != null)
//         {
//             cameraControl.SetCameraActive(false);
//         }

//         if (stoneRenderer != null && hoverMaterial != null)
//         {
//             stoneRenderer.material = hoverMaterial;
//         }
//     }

//     private void OnMouseExit()
//     {
//         if (!isDragging && cameraControl != null)
//         {
//             cameraControl.SetCameraActive(true);
//         }

//         if (!isDragging && stoneRenderer != null && defaultMaterial != null)
//         {
//             stoneRenderer.material = defaultMaterial;
//         }
//     }

//     private void OnMouseDown()
//     {
//         offset = transform.position - GetMouseWorldPosition();
//         isDragging = true;
//         currentPipe = GetComponentInParent<PipeController>();

//         if (stoneRenderer != null && hoverMaterial != null)
//         {
//             stoneRenderer.material = hoverMaterial;
//         }

//         if (cameraControl != null)
//         {
//             cameraControl.SetCameraActive(false);
//         }
//     }

//     private void OnMouseUp()
//     {
//         isDragging = false;

//         if (cameraControl != null)
//         {
//             cameraControl.SetCameraActive(true);
//         }

//         if (stoneRenderer != null && defaultMaterial != null)
//         {
//             stoneRenderer.material = defaultMaterial;
//         }
//     }

//     private void Update()
//     {
//         if (isDragging && currentPipe != null)
//         {
//             Vector3 mousePosition = GetMouseWorldPosition() + offset;
//             Vector3 pipeCenter = currentPipe.GetPipeCenter();
//             float pipeLength = currentPipe.GetPipeLength() / 2;

//             Vector3 newPosition;
//             if (currentPipe.IsHorizontalX)
//             {
//                 float clampedX = Mathf.Clamp(mousePosition.x, pipeCenter.x - pipeLength, pipeCenter.x + pipeLength);
//                 newPosition = new Vector3(clampedX, transform.position.y, transform.position.z);
//             }
//             else if (currentPipe.IsHorizontalZ)
//             {
//                 float clampedZ = Mathf.Clamp(mousePosition.z, pipeCenter.z - pipeLength, pipeCenter.z + pipeLength);
//                 newPosition = new Vector3(transform.position.x, transform.position.y, clampedZ);
//             }
//             else if (currentPipe.IsVertical)
//             {
//                 float clampedY = Mathf.Clamp(mousePosition.y, pipeCenter.y - pipeLength, pipeCenter.y + pipeLength);
//                 newPosition = new Vector3(transform.position.x, clampedY, transform.position.z);
//             }
//             else
//             {
//                 return;
//             }

//             targetPosition = newPosition;
//             isMoving = true;

//             UpdateSudahSampai();
//         }

//         if (isMoving)
//         {
//             transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);

//             // Update 'sudahSampai' continuously as the stone approaches the target position
//             UpdateSudahSampai();

//             if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
//             {
//                 isMoving = false;
//             }
//         }
//     }

//     private void UpdateSudahSampai()
//     {
//         if (currentPipe == null)
//             return;

//         Vector3 pipeCenter = currentPipe.GetPipeCenter();
//         float pipeLength = currentPipe.GetPipeLength() / 2;

//         bool edgeReached = false;
//         float threshold = 0.05f; // Small threshold value for better precision

//         if (currentPipe.IsHorizontalX)
//         {
//             edgeReached = Mathf.Abs(transform.position.x - (pipeCenter.x - pipeLength)) < threshold ||
//                           Mathf.Abs(transform.position.x - (pipeCenter.x + pipeLength)) < threshold;
//         }
//         else if (currentPipe.IsHorizontalZ)
//         {
//             edgeReached = Mathf.Abs(transform.position.z - (pipeCenter.z - pipeLength)) < threshold ||
//                           Mathf.Abs(transform.position.z - (pipeCenter.z + pipeLength)) < threshold;
//         }
//         else if (currentPipe.IsVertical)
//         {
//             edgeReached = Mathf.Abs(transform.position.y - (pipeCenter.y - pipeLength)) < threshold ||
//                           Mathf.Abs(transform.position.y - (pipeCenter.y + pipeLength)) < threshold;
//         }

//         if (edgeReached)
//         {
//             if (!sudahSampai)
//             {
//                 sudahSampai = true;
//                 Debug.Log("Bola sudah sampai");
//             }
//         }
//         else
//         {
//             if (sudahSampai)
//             {
//                 sudahSampai = false;
//                 Debug.Log("Bola belum sampai");
//             }
//         }
//     }




//     private Vector3 GetMouseWorldPosition()
//     {
//         Vector3 mousePoint = Input.mousePosition;
//         mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
//         return Camera.main.ScreenToWorldPoint(mousePoint);
//     }
// }
