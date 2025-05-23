using UnityEngine;
using TMPro;

public class PlayerRayManager : MonoBehaviour
{
    [Header("Raycast Settings")]
    public LayerMask layerMask;
    public float maxCheckDistance = 5f;

    [Header("UI References")]
    public TextMeshProUGUI infoText; // ������ ������ ǥ���� �ؽ�Ʈ

    private GameObject curInteractGameObject; // ���� ��ȣ�ۿ��� ���� ������Ʈ

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

            // Item ���̾ ó��
            if (hitObj.layer == itemLayer)
            {
                // ���ο� ������Ʈ ���� ��
                if (hitObj != curInteractGameObject)
                {
                    curInteractGameObject = hitObj;
                    // Item ��ũ��Ʈ���� ������ ������ UI�� ǥ��
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

        // Raycast�� ���� �ʰų� �ٸ� ���̾��� �� UI �ʱ�ȭ
        if (curInteractGameObject != null)
        {
            curInteractGameObject = null;
            infoText.text = "";
        }
    }

    void OnDrawGizmos() // Gizmos�� ����Ͽ� Raycast �ð�ȭ

    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * maxCheckDistance);
    }
}
