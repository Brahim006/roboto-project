using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkWorkerPlatform : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    private void OnTriggerStay(Collider other)
    {
        if(
            Input.GetKeyDown(KeyCode.E) &&
            !levelManager.IsHeadStucked() &&
            levelManager.IsLeglessWorkerActive() &&
            other.gameObject.TryGetComponent<PlataformerPlayer>(out PlataformerPlayer player)
          )
        {
            player.PressButton(transform.position);
            levelManager.OnTutorialLevelCompletion();
        }
    }
}
