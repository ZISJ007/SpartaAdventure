using UnityEngine;
using TMPro;

public class PlayerRayManager : MonoBehaviour
{
    public LayerMask layerMask;
    public float maxCheckDistance = 5f;
    private GameObject curInteractGameObject; // ���� ��ȣ�ۿ��� �� �ִ� ���� ������Ʈ

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
                curInteractGameObject = hitObj; // Raycast�� �´� ������Ʈ�� �ٸ��� ��ü
                Debug.Log("Item Hit: " + hitObj.name);
            }
        }
        else if (curInteractGameObject != null)
        {
            curInteractGameObject = null; // Raycast�� ���� ������ null�� �ʱ�ȭ
        }
    }

    void OnDrawGizmos() //������ Raycast �ð�ȭ
    {      
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * maxCheckDistance);
    }
}
