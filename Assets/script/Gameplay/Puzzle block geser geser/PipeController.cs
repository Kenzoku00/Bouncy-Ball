// using UnityEngine;

// public class PipeController : MonoBehaviour
// {
//     [SerializeField] private bool isHorizontalX;
//     [SerializeField] private bool isHorizontalZ;
//     [SerializeField] private bool isVertical;

//     [SerializeField] private float manualLength = 4f;
//     [SerializeField] private Vector3 centerOffset = Vector3.zero;

//     private BoxCollider boxCollider;

//     private void Awake()
//     {
//         boxCollider = GetComponent<BoxCollider>();
//         UpdatePipeSettings();
//     }

//     private void UpdatePipeSettings()
//     {
//         if (isHorizontalX)
//         {
//             manualLength = 4f;
//             centerOffset = new Vector3(-5f, 0f, 0f);
//         }
//         else if (isHorizontalZ)
//         {
//             manualLength = 4f;
//             centerOffset = new Vector3(0f, 0f, -5f);
//         }
//         else if (isVertical)
//         {
//             manualLength = 4f;
//             centerOffset = new Vector3(0f, 1f, 0f);
//         }
//     }

//     public bool IsHorizontalX => isHorizontalX;
//     public bool IsHorizontalZ => isHorizontalZ;
//     public bool IsVertical => isVertical;

//     public float GetPipeLength()
//     {
//         return manualLength;
//     }

//     public Vector3 GetPipeCenter()
//     {
//         Vector3 pipeCenter = transform.position + centerOffset;

//         if (isHorizontalX)
//         {
//             pipeCenter += new Vector3(GetPipeLength() / 2, 0, 0);
//         }
//         else if (isHorizontalZ)
//         {
//             pipeCenter += new Vector3(0, 0, GetPipeLength() / 2);
//         }
//         else if (isVertical)
//         {
//             pipeCenter += new Vector3(0, GetPipeLength() / 2, 0);
//         }

//         return pipeCenter;
//     }
// }
