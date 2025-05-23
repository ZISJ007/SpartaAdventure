using UnityEngine;
using TMPro;

public class PlayerRayManager : MonoBehaviour
{
    [Header("Raycast Settings")]
    public LayerMask layerMask;
    public float maxCheckDistance = 5f;

    [Header("UI References")]
    public TextMeshProUGUI infoText; // 아이템 정보를 표시할 텍스트

    private GameObject curInteractGameObject; // 현재 상호작용할 게임 오브젝트

    void Update()
    {
        CheckInfo();
    }

    void CheckInfo()
    {
        if (curInteractGameObject == null && !string.IsNullOrEmpty(infoText.text))
        {
            infoText.text = "";
        }
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
        {
            var hitObj = hit.collider.gameObject;
            int itemLayer = LayerMask.NameToLayer("Item");

            // Item 레이어만 처리
            if (hitObj.layer == itemLayer)
            {
                // 새로운 오브젝트 감지 시
                if (hitObj != curInteractGameObject)
                {
                    curInteractGameObject = hitObj;
                    // Item 스크립트에서 데이터 가져와 UI에 표시
                    var item = hitObj.GetComponent<Item>();
                    if (item != null && item.itemData != null)
                    {
                        infoText.text = item.itemData.itemName + "\n" + item.itemData.itemDescription;
                    }
                    else
                    {
                        infoText.text = "";
                    }
                }
                return;
            }

        }

        // Raycast에 맞지 않거나 다른 레이어일 때 UI 초기화
        if (curInteractGameObject != null)
        {
            curInteractGameObject = null;
            infoText.text = "";
        }
    }

    void OnDrawGizmos() // Gizmos를 사용하여 Raycast 시각화

    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * maxCheckDistance);
    }
}
