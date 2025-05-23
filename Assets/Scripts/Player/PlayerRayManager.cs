using UnityEngine;
using TMPro;

public class PlayerRayManager : MonoBehaviour
{
    public LayerMask layerMask;
    public float maxCheckDistance = 5f;
    private GameObject curInteractGameObject; // 현재 상호작용할 수 있는 게임 오브젝트

    void Update()
    {
        CheckInfo();
    }

    void CheckInfo()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
        {
            GameObject hitObj = hit.collider.gameObject;
            int itemLayer = LayerMask.NameToLayer("Item");

            if (hitObj != curInteractGameObject && hitObj.layer == itemLayer)
            {
                curInteractGameObject = hitObj; // Raycast에 맞는 오브젝트가 다르면 교체
                Debug.Log("Item Hit: " + hitObj.name);
            }
        }
        else if (curInteractGameObject != null)
        {
            curInteractGameObject = null; // Raycast에 맞지 않으면 null로 초기화
        }
    }

    void OnDrawGizmos() //씬에서 Raycast 시각화
    {      
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * maxCheckDistance);
    }
}
